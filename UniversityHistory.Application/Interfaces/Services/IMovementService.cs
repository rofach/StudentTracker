using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IMovementService
{
    Task<StudentMovementDto> GetMovementsAsync(Guid studentId, CancellationToken ct = default);
    Task<PagedResult<ActiveAcademicDifferenceDto>> GetActiveAcademicDifferenceAsync(
        string? studentName,
        string? disciplineName,
        string? status,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default);
    Task<PagedResult<InternalTransferJournalItemDto>> GetInternalTransferJournalAsync(
        string? studentName,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        bool onlyWithPendingDifference = false,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default);
    Task<ExternalTransferDto> CreateTransferAsync(Guid studentId, CreateTransferDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CreateLeaveAsync(Guid studentId, CreateLeaveDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> CloseLeaveAsync(Guid leaveId, CloseAcademicLeaveDto dto, CancellationToken ct = default);
    Task<AcademicLeaveDto> UpdateLeaveAsync(Guid leaveId, UpdateAcademicLeaveDto dto, CancellationToken ct = default);
}

