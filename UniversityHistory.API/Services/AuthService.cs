using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UniversityHistory.API.Auth;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Infrastructure.Identity;

namespace UniversityHistory.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<AuthSessionDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(dto.Login)
            ?? await _userManager.FindByEmailAsync(dto.Login)
            ?? throw new DomainException("Невірний логін або пароль.");

        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            throw new DomainException("Невірний логін або пароль.");

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count == 0)
            throw new DomainException("Для користувача не налаштовано роль.");

        var currentUser = MapCurrentUser(user, roles);
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
        var accessToken = BuildJwt(user, roles, expiresAt);

        return new AuthSessionDto(accessToken, expiresAt, currentUser);
    }

    public async Task<CurrentUserDto> GetCurrentUserAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count == 0)
            throw new DomainException("Для користувача не налаштовано роль.");

        return MapCurrentUser(user, roles);
    }

    private string BuildJwt(ApplicationUser user, IList<string> roles, DateTimeOffset expiresAt)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? user.Email ?? user.Id.ToString()),
        };

        if (!string.IsNullOrWhiteSpace(user.Email))
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

        if (user.StudentId.HasValue)
            claims.Add(new Claim(AuthClaimTypes.StudentId, user.StudentId.Value.ToString()));

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static CurrentUserDto MapCurrentUser(ApplicationUser user, IEnumerable<string> roles)
    {
        var role = roles.Contains(AuthRoles.Admin, StringComparer.Ordinal)
            ? AuthRoles.Admin
            : roles.First();

        return new CurrentUserDto(
            user.Id,
            user.UserName ?? user.Email ?? user.Id.ToString(),
            user.Email,
            role,
            user.StudentId);
    }
}
