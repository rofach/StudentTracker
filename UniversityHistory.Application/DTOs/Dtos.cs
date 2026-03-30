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
    int AcademicYearStart,
    string AcademicYearLabel,
    string GradeValue,
    DateOnly AssessmentDate
);

public record AverageGradeDto(
    decimal? Average,
    int GradeCount,
    string? AcademicYearLabel
);

public record StudentMovementDto(
    IEnumerable<AcademicLeaveDto> Leaves,
    IEnumerable<ExternalTransferDto> Transfers
);

public record StudentUpdateDto(
    string FirstName,
    string LastName,
    DateOnly? BirthDate,
    string? Email,
    string? Phone
);

public record ChangeStatusDto(string Status);

public record MoveStudentDto(
    int NewGroupId,
    int? NewSubgroupId,
    DateOnly MoveDate,
    string ReasonEnd,
    string ReasonStart
);

public record AssignSubgroupDto(int SubgroupId);

public record AssignPlanDto(int PlanId, DateOnly DateFrom);

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

public record EnrollmentSummaryDto(
    int EnrollmentId,
    int GroupId,
    string GroupCode,
    string? Faculty,
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

public record CreateDisciplineDto(string DisciplineName);

public record UpdateDisciplineDto(string DisciplineName);

public record DisciplineDto(
    int DisciplineId,
    string DisciplineName
);

public record StudyPlanDto(
    int PlanId,
    string SpecialtyCode,
    string? PlanName,
    DateOnly ValidFrom
);

public record CreateStudyPlanDto(
    string SpecialtyCode,
    string? PlanName,
    DateOnly ValidFrom
);

public record UpdateStudyPlanDto(
    string SpecialtyCode,
    string? PlanName,
    DateOnly ValidFrom
);

public record PlanDisciplineDto(
    int PlanId,
    int DisciplineId,
    string DisciplineName,
    int SemesterNo,
    string ControlType,
    int Hours,
    decimal Credits
);

public record AddPlanDisciplineDto(
    int DisciplineId,
    int SemesterNo,
    string ControlType,
    int Hours,
    decimal Credits
);

public record UpdatePlanDisciplineDto(
    int SemesterNo,
    string ControlType,
    int Hours,
    decimal Credits
);