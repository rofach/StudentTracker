namespace UniversityHistory.Application.DTOs;

public record EnrollStudentDto(
    int StudentId,
    int GroupId,
    int? SubgroupId,
    DateOnly DateFrom,
    string ReasonStart
);

public record CloseEnrollmentDto(
    DateOnly DateTo,
    string ReasonEnd
);

public record GroupDto(
    int GroupId,
    string GroupCode,
    string? Faculty
);

public record ActiveGroupDto(
    int GroupId,
    string GroupCode,
    string? Faculty,
    DateOnly DateCreated,
    DateOnly? DateClosed
);

public record GroupStudentDto(
    int EnrollmentId,
    int StudentId,
    string FirstName,
    string LastName,
    string? Email,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record StudentCurrentGroupDto(
    int EnrollmentId,
    int GroupId,
    string GroupCode,
    string? Faculty,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record GroupCompositionMemberDto(
    int StudentId,
    string FirstName,
    string LastName,
    string? Email,
    int? SubgroupId,
    string? SubgroupName,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record ClassmateDto(
    int ClassmateStudentId,
    string FirstName,
    string LastName,
    int GroupId,
    string GroupCode,
    DateOnly SharedFrom,
    DateOnly? SharedTo
);

public record MoveStudentDto(
    int NewGroupId,
    int? NewSubgroupId,
    DateOnly MoveDate,
    string ReasonEnd,
    string ReasonStart
);

public record AssignSubgroupDto(int SubgroupId);
