namespace UniversityHistory.Application.DTOs;

public record GradeDto(
    Guid GradeId,
    Guid CourseEnrollmentId,
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
    Guid CourseEnrollmentId,
    Guid DisciplineId,
    string DisciplineName,
    int SemesterNo,
    int AcademicYearStart,
    string AcademicYearLabel,
    bool HasGrade
);

public record UpsertGradeDto(
    string GradeValue,
    DateOnly AssessmentDate
);

