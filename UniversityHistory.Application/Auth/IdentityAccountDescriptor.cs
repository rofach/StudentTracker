namespace UniversityHistory.Application.Auth;

public record IdentityAccountDescriptor(
    Guid UserId,
    string UserName,
    string? Email,
    Guid? StudentId,
    IReadOnlyCollection<string> Roles
);
