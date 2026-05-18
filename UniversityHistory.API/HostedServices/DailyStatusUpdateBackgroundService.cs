using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.API.HostedServices;

public class DailyStatusUpdateBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DailyStatusUpdateBackgroundService> _logger;

    public DailyStatusUpdateBackgroundService(
        IServiceProvider serviceProvider, 
        ILogger<DailyStatusUpdateBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DailyStatusUpdateBackgroundService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await UpdateStudentStatusesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating student statuses.");
            }

            var nextRunTime = DateTime.Today.AddDays(1);
            var delay = nextRunTime - DateTime.Now;
            if (delay < TimeSpan.Zero) delay = TimeSpan.FromHours(24);

            _logger.LogInformation("Next status update scheduled in {DelayHours:F2} hours.", delay.TotalHours);
            await Task.Delay(delay, stoppingToken);
        }
    }

    private async Task UpdateStudentStatusesAsync(CancellationToken ct)
    {
        _logger.LogInformation("Running daily student status update...");
        
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<UniversityDbContext>();
        
        var today = DateOnly.FromDateTime(DateTime.Today);
        var updatedCount = 0;

        var studentsStartingLeave = await db.AcademicLeaves
            .Where(l => l.StartDate <= today && (l.EndDate == null || l.EndDate >= today))
            .Select(l => l.Enrollment.Student)
            .Where(s => s.Status != StudentStatus.OnLeave)
            .Distinct()
            .ToListAsync(ct);

        foreach (var student in studentsStartingLeave)
        {
            student.Status = StudentStatus.OnLeave;
            updatedCount++;
        }

        var studentsOnLeave = await db.Students
            .Where(s => s.Status == StudentStatus.OnLeave)
            .ToListAsync(ct);

        foreach (var student in studentsOnLeave)
        {
            var hasActiveLeaveToday = await db.AcademicLeaves
                .AnyAsync(l => l.Enrollment.StudentId == student.StudentId 
                            && l.StartDate <= today 
                            && (l.EndDate == null || l.EndDate >= today), ct);

            if (!hasActiveLeaveToday)
            {
                student.Status = StudentStatus.Active;
                updatedCount++;
            }
        }

        if (updatedCount > 0)
        {
            await db.SaveChangesAsync(ct);
            _logger.LogInformation("Successfully updated {Count} student statuses.", updatedCount);
        }
        else
        {
            _logger.LogInformation("No student statuses needed updating.");
        }
    }
}
