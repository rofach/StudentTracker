using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UniversityHistory.API.Auth;
using UniversityHistory.Infrastructure.Identity;

namespace UniversityHistory.API.Services;

public class IdentitySeeder
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BootstrapAuthOptions _options;

    public IdentitySeeder(
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<ApplicationUser> userManager,
        IOptions<BootstrapAuthOptions> options)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _options = options.Value;
    }

    public async Task SeedAsync()
    {
        await EnsureRoleAsync(AuthRoles.Admin);
        await EnsureRoleAsync(AuthRoles.Student);

        foreach (var account in _options.Users)
        {
            if (string.IsNullOrWhiteSpace(account.UserName) || string.IsNullOrWhiteSpace(account.Password))
                continue;

            var user = await _userManager.FindByNameAsync(account.UserName)
                ?? await _userManager.FindByEmailAsync(account.Email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = account.UserName,
                    Email = account.Email,
                    EmailConfirmed = true,
                    StudentId = account.StudentId,
                };

                var createResult = await _userManager.CreateAsync(user, account.Password);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join("; ", createResult.Errors.Select(error => error.Description));
                    throw new InvalidOperationException($"Не вдалося створити bootstrap-користувача {account.UserName}: {errors}");
                }
            }
            else
            {
                var requiresUpdate = false;

                if (!string.Equals(user.Email, account.Email, StringComparison.OrdinalIgnoreCase))
                {
                    user.Email = account.Email;
                    user.NormalizedEmail = _userManager.NormalizeEmail(account.Email);
                    requiresUpdate = true;
                }

                if (user.StudentId != account.StudentId)
                {
                    user.StudentId = account.StudentId;
                    requiresUpdate = true;
                }

                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    requiresUpdate = true;
                }

                if (requiresUpdate)
                {
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        var errors = string.Join("; ", updateResult.Errors.Select(error => error.Description));
                        throw new InvalidOperationException($"Не вдалося оновити bootstrap-користувача {account.UserName}: {errors}");
                    }
                }
            }

            if (!await _userManager.IsInRoleAsync(user, account.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, account.Role);
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join("; ", roleResult.Errors.Select(error => error.Description));
                    throw new InvalidOperationException($"Не вдалося призначити роль {account.Role} користувачу {account.UserName}: {errors}");
                }
            }
        }
    }

    private async Task EnsureRoleAsync(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            return;

        var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(error => error.Description));
            throw new InvalidOperationException($"Не вдалося створити роль {roleName}: {errors}");
        }
    }
}
