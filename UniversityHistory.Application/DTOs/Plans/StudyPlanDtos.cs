namespace UniversityHistory.Application.DTOs;

public record GroupPlanAssignmentDto(
    int GroupPlanAssignmentId,
    int GroupId,
    int PlanId,
    string SpecialtyCode,
    string? PlanName,
    DateOnly DateFrom,
    DateOnly? DateTo
);

public record AssignGroupPlanDto(int PlanId, DateOnly DateFrom);

public record ChangeGroupPlanDto(int NewPlanId, DateOnly NewPlanDateFrom);

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
