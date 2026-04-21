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

public record SubgroupDto(
    Guid SubgroupId,
    string SubgroupName
);

public record GroupStudentDto(
    Guid EnrollmentId,
    Guid StudentId,
    string FirstName,
    string LastName,
    string? Email,
    Guid? SubgroupId,
    string? SubgroupName,
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
    Guid? SubgroupId,
    string? SubgroupName,
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
    Guid? SubgroupId,
    string? SubgroupName,
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

public record AcademicDifferenceItemDto(
    Guid DifferenceItemId,
    Guid TransferId,
    Guid PlanDisciplineId,
    string DisciplineName,
    int SemesterNo,
    string Status,
    string? Notes
);

public record UpdateDifferenceItemDto(string Status, string? Notes);

public record StudentGroupTransferDto(
    Guid TransferId,
    Guid StudentId,
    Guid OldEnrollmentId,
    Guid NewEnrollmentId,
    DateOnly TransferDate,
    string Reason
);

public record StudentGroupTransferDetailDto(
    Guid TransferId,
    Guid StudentId,
    Guid OldEnrollmentId,
    string OldGroupCode,
    Guid NewEnrollmentId,
    string NewGroupCode,
    DateOnly TransferDate,
    string Reason,
    IEnumerable<AcademicDifferenceItemDto> DifferenceItems
);
