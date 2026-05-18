namespace UniversityHistory.Application.Queries.GetStudentDisciplines;

public record GetStudentDisciplinesQuery(Guid StudentId, bool CurrentPlanOnly = false);

