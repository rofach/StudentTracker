using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class StudyPlanService : IStudyPlanService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IStudyPlanRepository _planRepo;

    public StudyPlanService(IStudentRepository studentRepo, IStudyPlanRepository planRepo)
    {
        _studentRepo = studentRepo;
        _planRepo    = planRepo;
    }

    public async Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var assignments = await _planRepo.GetAssignmentsByStudentIdAsync(studentId, ct);
        return assignments.Select(a => new StudyPlanAssignmentDto(
            a.AssignmentId, a.Plan.SpecialtyCode, a.Plan.PlanName, a.DateFrom, a.DateTo));
    }

    public async Task<StudyPlanAssignmentDto> AssignPlanAsync(int studentId, AssignPlanDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var plan = await _planRepo.GetPlanByIdAsync(dto.PlanId, ct)
            ?? throw new NotFoundException(nameof(StudyPlan), dto.PlanId);

        var open = await _planRepo.GetOpenAssignmentByStudentIdAsync(studentId, ct);
        if (open is not null)
        {
            if (dto.DateFrom <= open.DateFrom)
                throw new DomainException("New plan DateFrom must be after the current plan's start date.");

            open.DateTo = dto.DateFrom.AddDays(-1);
            await _planRepo.UpdateAssignmentAsync(open, ct);
        }

        var assignment = new StudentPlanAssignment
        {
            StudentId = studentId,
            PlanId    = dto.PlanId,
            DateFrom  = dto.DateFrom
        };

        await _planRepo.AddAssignmentAsync(assignment, ct);
        return new StudyPlanAssignmentDto(assignment.AssignmentId, plan.SpecialtyCode, plan.PlanName, assignment.DateFrom, null);
    }
}
