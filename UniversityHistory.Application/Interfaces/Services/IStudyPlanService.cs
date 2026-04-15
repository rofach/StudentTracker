using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudyPlanService
{
    Task<IEnumerable<StudyPlanDto>> GetAllPlansAsync(CancellationToken ct = default);
    Task<StudyPlanDto?> GetPlanByIdAsync(Guid planId, CancellationToken ct = default);
    Task<StudyPlanDto> CreatePlanAsync(CreateStudyPlanDto dto, CancellationToken ct = default);
    Task<StudyPlanDto> UpdatePlanAsync(Guid planId, UpdateStudyPlanDto dto, CancellationToken ct = default);
    Task DeletePlanAsync(Guid planId, CancellationToken ct = default);

    Task<IEnumerable<PlanDisciplineDto>> GetPlanDisciplinesAsync(Guid planId, CancellationToken ct = default);
    Task<PlanDisciplineDto> AddPlanDisciplineAsync(Guid planId, AddPlanDisciplineDto dto, CancellationToken ct = default);
    Task<PlanDisciplineDto> UpdatePlanDisciplineAsync(Guid planId, Guid disciplineId, UpdatePlanDisciplineDto dto, CancellationToken ct = default);
    Task DeletePlanDisciplineAsync(Guid planId, Guid disciplineId, CancellationToken ct = default);

    Task<IEnumerable<GroupPlanAssignmentDto>> GetGroupPlanHistoryAsync(Guid groupId, CancellationToken ct = default);
    Task<GroupPlanAssignmentDto> AssignGroupPlanAsync(Guid groupId, AssignGroupPlanDto dto, CancellationToken ct = default);
    Task<GroupPlanAssignmentDto> ChangeGroupPlanAsync(Guid groupId, ChangeGroupPlanDto dto, CancellationToken ct = default);
}

