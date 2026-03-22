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
    int CreationYear,
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

public record StudyPlanAssignmentDto(
    int AssignmentId,
    string SpecialtyCode,
    string? PlanName,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record GradeDto(
    int GradeId,
    string DisciplineName,
    int SemesterNo,
    int AttemptNo,
    string GradeValue,
    DateOnly AssessmentDate
);

public record StudentMovementDto(
    IEnumerable<AcademicLeaveDto> Leaves,
    IEnumerable<ExternalTransferDto> Transfers
);
