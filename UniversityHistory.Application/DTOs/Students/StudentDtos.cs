namespace UniversityHistory.Application.DTOs;

public record StudentDto(
    Guid StudentId,
    string FirstName,
    string LastName,
    string? Patronymic,
    DateOnly? BirthDate,
    string? Email,
    string? Phone,
    string Status
);

public record StudentCreateDto(
    string FirstName,
    string LastName,
    string? Patronymic,
    DateOnly? BirthDate,
    string? Email,
    string? Phone
);

public record StudentUpdateDto(
    string FirstName,
    string LastName,
    string? Patronymic,
    DateOnly? BirthDate,
    string? Email,
    string? Phone
);

public record ChangeStatusDto(string Status);

public record EnrollmentSummaryDto(
    Guid EnrollmentId,
    Guid GroupId,
    string GroupCode,
    string DepartmentName,
    string AcademicUnitName,
    DateOnly DateFrom,
    DateOnly? DateTo,
    Guid? SubgroupId,
    string? SubgroupName
);

public record StudentDetailDto(
    Guid StudentId,
    string FirstName,
    string LastName,
    string? Patronymic,
    DateOnly? BirthDate,
    string? Email,
    string? Phone,
    string Status,
    bool IsOnAcademicLeave,
    IEnumerable<EnrollmentSummaryDto> Enrollments,
    IEnumerable<GroupPlanAssignmentDto> Plans,
    IEnumerable<AcademicLeaveDto> Leaves,
    IEnumerable<ExternalTransferDto> Transfers,
    IEnumerable<StudentInternalTransferSummaryDto> InternalTransfers
);

