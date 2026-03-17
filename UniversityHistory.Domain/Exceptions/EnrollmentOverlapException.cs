namespace UniversityHistory.Domain.Exceptions;

public class EnrollmentOverlapException : DomainException
{
    public EnrollmentOverlapException(int studentId, DateOnly dateFrom, DateOnly? dateTo)
        : base($"Student {studentId} already has an active enrollment overlapping the period {dateFrom} – {dateTo?.ToString() ?? "ongoing"}.") { }
}
