using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudentService
{
    Task<StudentDto?> GetByIdAsync(int studentId, CancellationToken ct = default);
    Task<IEnumerable<StudentDto>> GetAllAsync(CancellationToken ct = default);
    Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
    Task<IEnumerable<TimelineEventDto>> GetTimelineAsync(int studentId, CancellationToken ct = default);
    Task<IEnumerable<ClassmateDto>> GetClassmatesAsync(int studentId, CancellationToken ct = default);
    Task<StudentCurrentGroupDto?> GetGroupOnDateAsync(int studentId, DateOnly? date, CancellationToken ct = default);
}

public interface IGroupService
{
    Task<IEnumerable<GroupCompositionMemberDto>> GetCompositionAsync(int groupId, DateOnly? date = null, CancellationToken ct = default);
    Task<IEnumerable<ActiveGroupDto>> GetActiveGroupsAsync(DateOnly? date = null, CancellationToken ct = default);
    Task<IEnumerable<GroupStudentDto>> GetStudentsInGroupAsync(int groupId, DateOnly? date = null, CancellationToken ct = default);
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
    Task<IEnumerable<GradeDto>> GetGradesAsync(int studentId, CancellationToken ct = default);
}

public interface IEnrollmentService
{
    Task<int> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default);
    Task CloseEnrollmentAsync(int enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default);
}
