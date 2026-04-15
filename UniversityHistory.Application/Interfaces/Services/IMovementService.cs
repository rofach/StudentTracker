using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IMovementService
{
    Task<StudentMovementDto> GetMovementsAsync(int studentId, CancellationToken ct = default);
    Task<ExternalTransferDto> CreateTransferAsync(int studentId, CreateTransferDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CreateLeaveAsync(int studentId, CreateLeaveDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CloseLeaveAsync(int leaveId, CloseAcademicLeaveDto dto, CancellationToken ct = default);
}
