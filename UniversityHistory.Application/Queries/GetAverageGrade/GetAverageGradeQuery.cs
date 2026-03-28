namespace UniversityHistory.Application.Queries.GetAverageGrade;

public record GetAverageGradeQuery(int StudentId, int? SemesterNo, int? DisciplineId, int? AcademicYearStart);
