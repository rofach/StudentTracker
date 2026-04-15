using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IMovementService
{
    Task<StudentMovementDto> GetMovementsAsync(Guid studentId, CancellationToken ct = default);
    Task<ExternalTransferDto> CreateTransferAsync(Guid studentId, CreateTransferDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CreateLeaveAsync(Guid studentId, CreateLeaveDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CloseLeaveAsync(Guid leaveId, CloseAcademicLeaveDto dto, CancellationToken ct = default);
}

