namespace UniversityHistory.Application.DTOs;

public record EnrollStudentDto(
    Guid StudentId,
    Guid GroupId,
    Guid? SubgroupId,
    DateOnly DateFrom,
    string ReasonStart
);

public record CloseEnrollmentDto(
    DateOnly DateTo,
    string ReasonEnd
);

public record GroupDto(
    Guid GroupId,
    string GroupCode,
    string DepartmentName,
    string AcademicUnitName,
    string AcademicUnitType
);

public record ActiveGroupDto(
    Guid GroupId,
    string GroupCode,
    string DepartmentName,
    string AcademicUnitName,
    string AcademicUnitType,
    DateOnly DateCreated,
    DateOnly? DateClosed
);

public record GroupStudentDto(
    Guid EnrollmentId,
    Guid StudentId,
    string FirstName,
    string LastName,
    string? Email,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record StudentCurrentGroupDto(
    Guid EnrollmentId,
    Guid GroupId,
    string GroupCode,
    string DepartmentName,
    string AcademicUnitName,
    string AcademicUnitType,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record GroupCompositionMemberDto(
    Guid StudentId,
    string FirstName,
    string LastName,
    string? Email,
    Guid? SubgroupId,
    string? SubgroupName,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record ClassmateDto(
    Guid ClassmateStudentId,
    string FirstName,
    string LastName,
    Guid GroupId,
    string GroupCode,
    DateOnly SharedFrom,
    DateOnly? SharedTo
);

public record MoveStudentDto(
    Guid NewGroupId,
    Guid? NewSubgroupId,
    DateOnly MoveDate,
    string ReasonEnd,
    string ReasonStart
);

public record AssignSubgroupDto(Guid SubgroupId);

public record MoveStudentToSubgroupDto(Guid NewSubgroupId, DateOnly MoveDate, string Reason);


