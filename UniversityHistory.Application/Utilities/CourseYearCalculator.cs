namespace UniversityHistory.Application.Utilities;

public static class CourseYearCalculator
{
    public static int? Calculate(DateOnly groupDateCreated, DateOnly? onDate = null)
    {
        var reference = onDate ?? DateOnly.FromDateTime(DateTime.Today);

        int groupAcademicYearStart = groupDateCreated.Month >= 9
            ? groupDateCreated.Year
            : groupDateCreated.Year - 1;

        int currentAcademicYearStart = reference.Month >= 9
            ? reference.Year
            : reference.Year - 1;

        int courseYear = currentAcademicYearStart - groupAcademicYearStart + 1;
        return courseYear >= 1 ? courseYear : null;
    }
}
