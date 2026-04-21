namespace UniversityHistory.Application.Rules;

public interface IStudyProcessRule
{
    Task EnsureEnrollmentModificationAllowedAsync(
        Guid enrollmentId,
        DateOnly operationDate,
        CancellationToken ct = default);

    Task EnsureStudentModificationAllowedAsync(
        Guid studentId,
        DateOnly operationDate,
        CancellationToken ct = default);
}
