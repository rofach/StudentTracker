namespace UniversityHistory.Application.DTOs;

public record TimelineEventDto(
    string EventType,
    string Description,
    DateOnly DateFrom,
    DateOnly? DateTo,
    string? GroupCode,
    string? DepartmentName,
    string? AcademicUnitName,
    string? AcademicUnitType
);

public record StudentInternalTransferSummaryDto(
    Guid TransferId,
    DateOnly TransferDate,
    string Reason,
    Guid OldEnrollmentId,
    string OldGroupCode,
    Guid NewEnrollmentId,
    string NewGroupCode,
    int DifferenceItemsTotal,
    int DifferenceItemsPending,
    int DifferenceItemsCompleted,
    int DifferenceItemsWaived
);

public record AcademicLeaveDto(
    Guid LeaveId,
    DateOnly StartDate,
    DateOnly? EndDate,
    string? Reason,
    string? ReturnReason
);

public record ExternalTransferDto(
    Guid TransferId,
    string TransferType,
    DateOnly TransferDate,
    string InstitutionName,
    string? Notes
);

public record InstitutionDto(
    Guid InstitutionId,
    string InstitutionName
);

public record StudentMovementDto(
    IEnumerable<AcademicLeaveDto> Leaves,
    IEnumerable<ExternalTransferDto> Transfers,
    IEnumerable<StudentInternalTransferSummaryDto> InternalTransfers
);

public record CreateTransferDto(
    Guid InstitutionId,
    string TransferType,
    DateOnly TransferDate,
    string? Notes
);

public record TransferStudentOutDto(
    Guid InstitutionId,
    DateOnly TransferDate,
    string ReasonEnd,
    string? Notes
);

public record ReturnStudentFromExternalTransferDto(
    Guid InstitutionId,
    Guid GroupId,
    Guid? SubgroupId,
    DateOnly DateFrom,
    string ReasonStart,
    string? Notes
);

public record CreateLeaveDto(
    Guid EnrollmentId,
    DateOnly StartDate,
    DateOnly? EndDate,
    string Reason,
    bool AllowRepeatedLeave = false
);

public record CloseAcademicLeaveDto(
    DateOnly EndDate,
    string? ReturnReason
);

public record UpdateAcademicLeaveDto(
    DateOnly StartDate,
    DateOnly? EndDate,
    string Reason,
    string? ReturnReason
);

