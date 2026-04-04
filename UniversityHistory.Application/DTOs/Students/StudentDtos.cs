namespace UniversityHistory.Application.DTOs;

public record StudentDto(
    int StudentId,
    string FirstName,
    string LastName,
    DateOnly? BirthDate,
    string? Email,
    string? Phone,
    string Status
);

public record StudentCreateDto(
    string FirstName,
    string LastName,
    DateOnly? BirthDate,
    string? Email,
    string? Phone
);

public record StudentUpdateDto(
    string FirstName,
    string LastName,
    DateOnly? BirthDate,
    string? Email,
    string? Phone
);

public record ChangeStatusDto(string Status);

public record EnrollmentSummaryDto(
    int EnrollmentId,
    int GroupId,
    string GroupCode,
    string DepartmentName,
    string AcademicUnitName,
    DateOnly DateFrom,
    DateOnly? DateTo,
    int? SubgroupId,
    string? SubgroupName
);

public record StudentDetailDto(
    int StudentId,
    string FirstName,
    string LastName,
    DateOnly? BirthDate,
    string? Email,
    string? Phone,
    string Status,
    IEnumerable<EnrollmentSummaryDto> Enrollments,
    IEnumerable<StudyPlanAssignmentDto> Plans,
    IEnumerable<AcademicLeaveDto> Leaves,
    IEnumerable<ExternalTransferDto> Transfers
);
