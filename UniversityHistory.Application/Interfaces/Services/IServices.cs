using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudentService
{
    Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default);
    Task<PagedResult<StudentDto>> GetAllAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
    Task<PagedResult<TimelineEventDto>> GetTimelineAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(int studentId, DateOnly? dateFrom, DateOnly? dateTo, CancellationToken ct = default);
    Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(int studentId, DateOnly? date, CancellationToken ct = default);
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
}

public interface IStudyPlanService
{
    Task<IEnumerable<StudyPlanAssignmentDto>> GetPlanAssignmentsAsync(int studentId, CancellationToken ct = default);
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
}
