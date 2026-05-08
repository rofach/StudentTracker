namespace UniversityHistory.Application.Auth;

public record IssuedAccessToken(
    string AccessToken,
    DateTimeOffset ExpiresAt
);
