namespace UniversityHistory.Application.DTOs;

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

public record StudentDisciplineOptionDto(
    int DisciplineId,
    string DisciplineName,
    int SemesterNo,
    bool HasGrade
);
