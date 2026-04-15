namespace UniversityHistory.Application.DTOs;

public record GroupPlanAssignmentDto(
    Guid GroupPlanAssignmentId,
    Guid GroupId,
    Guid PlanId,
    string SpecialtyCode,
    string? PlanName,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record AssignGroupPlanDto(Guid PlanId, DateOnly DateFrom);

public record ChangeGroupPlanDto(Guid NewPlanId, DateOnly NewPlanDateFrom);

public record StudyPlanDto(
    Guid PlanId,
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
    Guid PlanId,
    Guid DisciplineId,
    string DisciplineName,
    int SemesterNo,
    string ControlType,
    int Hours,
    decimal Credits
);

public record AddPlanDisciplineDto(
    Guid DisciplineId,
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

