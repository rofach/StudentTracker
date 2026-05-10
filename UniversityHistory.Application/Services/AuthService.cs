using UniversityHistory.Application.Auth;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Auth;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Exceptions;

namespace UniversityHistory.Application.Services;

public class AuthService : IAuthService
{
    private readonly IIdentityAccountManager _identityAccountManager;
    private readonly IAuthTokenIssuer _authTokenIssuer;

    public AuthService(
        IIdentityAccountManager identityAccountManager,
        IAuthTokenIssuer authTokenIssuer)
    {
        _identityAccountManager = identityAccountManager;
        _authTokenIssuer = authTokenIssuer;
    }

    public async Task<AuthSessionDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
    {
        var account = await _identityAccountManager.FindByLoginAsync(dto.Login, ct)
            ?? throw new DomainException("Невірний логін або пароль.");

        if (!await _identityAccountManager.CheckPasswordAsync(account.UserId, dto.Password, ct))
            throw new DomainException("Невірний логін або пароль.");

        EnsureHasRole(account);

        var currentUser = MapCurrentUser(account);
        var token = _authTokenIssuer.IssueAccessToken(account);

        return new AuthSessionDto(token.AccessToken, token.ExpiresAt, currentUser);
    }

    public async Task<CurrentUserDto> GetCurrentUserAsync(Guid userId, CancellationToken ct = default)
    {
        var account = await _identityAccountManager.FindByIdAsync(userId, ct)
            ?? throw new NotFoundException("ApplicationUser", userId);

        EnsureHasRole(account);
        return MapCurrentUser(account);
    }

    private static void EnsureHasRole(IdentityAccountDescriptor account)
    {
        if (account.Roles.Count == 0)
            throw new DomainException("Для користувача не налаштовано роль.");
    }

    private static CurrentUserDto MapCurrentUser(IdentityAccountDescriptor account)
    {
        var role = account.Roles.Contains(AuthRoles.Admin, StringComparer.Ordinal)
            ? AuthRoles.Admin
            : account.Roles.First();

        return new CurrentUserDto(
            account.UserId,
            account.UserName,
            account.Email,
            role,
            account.StudentId);
    }
}
