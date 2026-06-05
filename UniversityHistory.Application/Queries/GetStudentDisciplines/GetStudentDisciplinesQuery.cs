namespace UniversityHistory.Application.Queries.GetStudentDisciplines;

public record GetStudentDisciplinesQuery(Guid StudentId, Guid? PlanId = null);

