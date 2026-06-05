using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniversityHistory.API.Auth;
using UniversityHistory.API.Services;
using UniversityHistory.Application.Interfaces.Auth;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Services;
using UniversityHistory.Infrastructure.Data;
using UniversityHistory.Infrastructure.Identity;

namespace UniversityHistory.API.Extensions;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.Configure<BootstrapAuthOptions>(configuration.GetSection(BootstrapAuthOptions.SectionName));

        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
            ?? throw new InvalidOperationException("JWT configuration is missing.");
        var jwtSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey));

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UniversityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = jwtSigningKey,
                    ClockSkew = TimeSpan.FromMinutes(1),
                };
            });

        services.AddAuthorization();

        services.AddScoped<IIdentityAccountManager, UniversityHistory.Infrastructure.Identity.IdentityAccountManager>();
        services.AddScoped<IAuthTokenIssuer, JwtTokenIssuer>();
        services.AddScoped<IPasswordGenerator, UniversityHistory.Infrastructure.Identity.PasswordGenerator>();
        services.AddScoped<IAuthService, UniversityHistory.Application.Services.AuthService>();
        services.AddScoped<IStudentAccountService, UniversityHistory.Application.Services.StudentAccountService>();
        services.AddScoped<IdentitySeeder>();

        return services;
    }
}
