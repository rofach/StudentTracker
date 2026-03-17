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
        _planRepo = planRepo;
    }

    public async Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var assignments = await _planRepo.GetAssignmentsByStudentIdAsync(studentId, ct);
        return assignments.Select(a => new StudyPlanAssignmentDto(
            a.AssignmentId, a.Plan.SpecialtyCode, a.Plan.PlanName, a.DateFrom, a.DateTo));
    }
}
