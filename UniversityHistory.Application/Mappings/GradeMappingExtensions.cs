using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Application.Mappings;

public static class GradeMappingExtensions
{
    public static GradeDto ToDto(this GradeRecord grade, int semesterNo)
    {
        var academicYearStart = grade.CourseEnrollment.AcademicYearStart;

        return new GradeDto(
            grade.GradeId,
            grade.CourseEnrollment.Discipline.DisciplineName,
            semesterNo,
            academicYearStart,
            $"{academicYearStart}/{academicYearStart + 1}",
            grade.GradeValue,
            grade.AssessmentDate);
    }
}
