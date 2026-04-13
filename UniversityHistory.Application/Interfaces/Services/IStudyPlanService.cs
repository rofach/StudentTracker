using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudyPlanService
{
    // Study plan CRUD
    Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default);
    Task<StudyPlanDto?> GetPlanByIdAsync(int planId, CancellationToken ct = default);
    Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default);
    Task<StudyPlanDto> UpdatePlanAsync(int planId, UpdateStudyPlanDto dto, CancellationToken ct = default);
    Task DeletePlanAsync(int planId, CancellationToken ct = default);

    // Plan disciplines
    Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(int planId, CancellationToken ct = default);
    Task<PlanDisciplineDto> AddPlanDisciplineAsync(int planId, AddPlanDisciplineDto dto, CancellationToken ct = default);
    Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(int planId, int disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default);
    Task DeletePlanDisciplineAsync(int planId, int disciplineId, CancellationToken ct = default);

    // Group plan assignments
    Task<IEnumerable<GroupPlanAssignmentDto>> GetGroupPlanHistoryAsync(int groupId, CancellationToken ct = default);
    Task<GroupPlanAssignmentDto> AssignGroupPlanAsync(int groupId, AssignGroupPlanDto dto, CancellationToken ct = default);
    Task<GroupPlanAssignmentDto> ChangeGroupPlanAsync(int groupId, ChangeGroupPlanDto dto, CancellationToken ct = default);
}
