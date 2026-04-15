namespace UniversityHistory.Application.DTOs;

public record GradeDto(
    Guid GradeId,
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
    Guid DisciplineId,
    string DisciplineName,
    int SemesterNo,
    int AcademicYearStart,
    string AcademicYearLabel,
    bool HasGrade
);

