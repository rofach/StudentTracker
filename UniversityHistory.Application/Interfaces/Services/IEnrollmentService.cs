using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task<Guid> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default);
    Task CloseEnrollmentAsync(Guid enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default);
    Task MoveToGroupAsync(Guid studentId, MoveStudentDto dto, CancellationToken ct = default);
    Task AssignSubgroupAsync(Guid enrollmentId, AssignSubgroupDto dto, CancellationToken ct = default);
    Task MoveSubgroupAsync(Guid enrollmentId, MoveStudentToSubgroupDto dto, CancellationToken ct = default);
}


