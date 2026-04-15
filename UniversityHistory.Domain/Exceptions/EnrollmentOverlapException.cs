namespace UniversityHistory.Domain.Exceptions;

public class EnrollmentOverlapException : DomainException
{
    public EnrollmentOverlapException(Guid studentId, DateOnly dateFrom, DateOnly? dateTo)
        : base($"Student {studentId} already has an active enrollment overlapping the period {dateFrom} – {dateTo?.ToString() ?? "ongoing"}.") { }
}

