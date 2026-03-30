using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudyPlanService
{
    Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default);
    Task<StudyPlanAssignmentDto> AssignPlanAsync(int studentId, AssignPlanDto dto, CancellationToken ct = default);
    Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default);
    Task<StudyPlanDto?> GetPlanByIdAsync(int planId, CancellationToken ct = default);
    Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default);
    Task<StudyPlanDto> UpdatePlanAsync(int planId, UpdateStudyPlanDto dto, CancellationToken ct = default);
    Task DeletePlanAsync(int planId, CancellationToken ct = default);
    Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(int planId, CancellationToken ct = default);
    Task<PlanDisciplineDto> AddPlanDisciplineAsync(int planId, AddPlanDisciplineDto dto, CancellationToken ct = default);
    Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(int planId, int disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default);
    Task DeletePlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default);
}
