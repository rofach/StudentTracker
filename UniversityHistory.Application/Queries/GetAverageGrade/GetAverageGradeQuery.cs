namespace UniversityHistory.Application.Queries.GetAverageGrade;

public record GetAverageGradeQuery(Guid StudentId, int? SemesterNo, Guid? DisciplineId, int? AcademicYearStart);

