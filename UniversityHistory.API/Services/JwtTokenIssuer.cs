using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UniversityHistory.API.Auth;
using UniversityHistory.Application.Auth;
using UniversityHistory.Application.Interfaces.Auth;

namespace UniversityHistory.API.Services;

public class JwtTokenIssuer : IAuthTokenIssuer
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenIssuer(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public IssuedAccessToken IssueAccessToken(IdentityAccountDescriptor account)
    {
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, account.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, account.UserId.ToString()),
            new(ClaimTypes.Name, account.UserName),
        };

        if (!string.IsNullOrWhiteSpace(account.Email))
            claims.Add(new Claim(ClaimTypes.Email, account.Email));

        if (account.StudentId.HasValue)
            claims.Add(new Claim(API.Auth.AuthClaimTypes.StudentId, account.StudentId.Value.ToString()));

        claims.AddRange(account.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials);

        return new IssuedAccessToken(new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
