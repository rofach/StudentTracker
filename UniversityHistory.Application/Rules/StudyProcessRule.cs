using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Rules;

public class StudyProcessRule : IStudyProcessRule
{
    private readonly IUnitOfWork _unitOfWork;

    public StudyProcessRule(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task EnsureEnrollmentModificationAllowedAsync(
        Guid enrollmentId,
        DateOnly operationDate,
        CancellationToken ct = default)
    {
        if (await _unitOfWork.AcademicLeaves.HasActiveLeaveOnDateAsync(enrollmentId, operationDate, ct))
        {
            throw new DomainException("Cannot modify study process while the student is on academic leave.");
        }
    }

    public async Task EnsureStudentModificationAllowedAsync(
        Guid studentId,
        DateOnly operationDate,
        CancellationToken ct = default)
    {
        var enrollments = await _unitOfWork.Enrollments.GetByStudentIdAsync(studentId, ct);
        var activeEnrollment = enrollments
            .Where(enrollment => enrollment.DateFrom <= operationDate
                && (!enrollment.DateTo.HasValue || enrollment.DateTo.Value >= operationDate))
            .OrderByDescending(enrollment => enrollment.DateFrom)
            .FirstOrDefault();

        if (activeEnrollment is null)
        {
            return;
        }

        await EnsureEnrollmentModificationAllowedAsync(activeEnrollment.EnrollmentId, operationDate, ct);
    }
}
