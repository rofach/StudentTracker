using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudentService
{
    Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default);
    Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
    Task<StudentDto> UpdateAsync(int studentId, StudentUpdateDto dto, CancellationToken ct = default);
    Task ChangeStatusAsync(int studentId, ChangeStatusDto dto, CancellationToken ct = default);
    Task<StudentDetailDto> GetDetailAsync(int studentId, CancellationToken ct = default);
}

public interface IGroupService
{
    Task<PagedResult<GroupCompositionMemberDto>> GetCompositionAsync(int groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<IEnumerable<ActiveGroupDto>> GetActiveGroupsAsync(DateOnly? date = null, CancellationToken ct = default);
    Task<PagedResult<GroupStudentDto>> GetStudentsInGroupAsync(int groupId, DateOnly? date = null, int page = 1, int pageSize = 20, CancellationToken ct = default);
}

public interface IMovementService
{
    Task<StudentMovementDto> GetMovementsAsync(int studentId, CancellationToken ct = default);
    Task<ExternalTransferDto> CreateTransferAsync(int studentId, CreateTransferDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CreateLeaveAsync(int studentId, CreateLeaveDto dto, CancellationToken ct = default);
}

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

public interface IGradeService
{
    Task<PagedResult<GradeDto>> GetGradesAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<AverageGradeDto> GetAverageGradeAsync(int studentId, int? semesterNo, int? disciplineId, int? academicYearStart, CancellationToken ct = default);
}

public interface IEnrollmentService
{
    Task<int> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default);
    Task CloseEnrollmentAsync(int enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default);
    Task MoveToGroupAsync(int studentId, MoveStudentDto dto, CancellationToken ct = default);
    Task AssignSubgroupAsync(int enrollmentId, AssignSubgroupDto dto, CancellationToken ct = default);
}

public interface IDisciplineService
{
    Task<IEnumerable<DisciplineDto>> GetAllAsync(CancellationToken ct = default);
    Task<DisciplineDto?> GetByIdAsync(int disciplineId, CancellationToken ct = default);
    Task<DisciplineDto> CreateAsync(CreateDisciplineDto dto, CancellationToken ct = default);
    Task<DisciplineDto> UpdateAsync(int disciplineId, UpdateDisciplineDto dto, CancellationToken ct = default);
    Task DeleteAsync(int disciplineId, CancellationToken ct = default);
}