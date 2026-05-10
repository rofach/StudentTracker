namespace UniversityHistory.Application.DTOs;

public record LoginRequestDto(
    string Login,
    string Password
);

public record CurrentUserDto(
    Guid UserId,
    string UserName,
    string? Email,
    string Role,
    Guid? StudentId
);

public record AuthSessionDto(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    CurrentUserDto User
);
