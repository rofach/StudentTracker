namespace UniversityHistory.Application.DTOs;

public record TimelineEventDto(
    string EventType,
    string Description,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record AcademicLeaveDto(
    int LeaveId,
    DateOnly StartDate,
    DateOnly? EndDate,
    string? Reason
);

public record ExternalTransferDto(
    int TransferId,
    string TransferType,
    DateOnly TransferDate,
    string InstitutionName,
    string? Notes
);

public record StudentMovementDto(
    IEnumerable<AcademicLeaveDto> Leaves,
    IEnumerable<ExternalTransferDto> Transfers
);

public record CreateTransferDto(
    int InstitutionId,
    string TransferType,
    DateOnly TransferDate,
    string? Notes
);

public record CreateLeaveDto(
    int EnrollmentId,
    DateOnly StartDate,
    string? Reason
);
