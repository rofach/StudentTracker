using Microsoft.Extensions.Options;
using UniversityHistory.API.Auth;
using UniversityHistory.Application.Interfaces.Auth;

namespace UniversityHistory.API.Services;

public class IdentitySeeder
{
    private readonly IIdentityAccountManager _identityAccountManager;
    private readonly BootstrapAuthOptions _options;

    public IdentitySeeder(
        IIdentityAccountManager identityAccountManager,
        IOptions<BootstrapAuthOptions> options)
    {
        _identityAccountManager = identityAccountManager;
        _options = options.Value;
    }

    public async Task SeedAsync()
    {
        await _identityAccountManager.EnsureRoleExistsAsync(AuthRoles.Admin);
        await _identityAccountManager.EnsureRoleExistsAsync(AuthRoles.Student);

        foreach (var account in _options.Users)
        {
            if (string.IsNullOrWhiteSpace(account.UserName) || string.IsNullOrWhiteSpace(account.Password))
                continue;

            var user = await _identityAccountManager.FindByLoginAsync(account.UserName)
                ?? await _identityAccountManager.FindByLoginAsync(account.Email);

            if (user is null)
            {
                user = await _identityAccountManager.CreateAccountAsync(
                    account.UserName,
                    account.Email,
                    account.Password,
                    account.StudentId,
                    emailConfirmed: true);
            }
            else
            {
                await _identityAccountManager.UpdateAccountAsync(
                    user.UserId,
                    account.UserName,
                    account.Email,
                    account.StudentId,
                    emailConfirmed: true);
            }

            await _identityAccountManager.EnsureRoleAssignedAsync(user.UserId, account.Role);
        }
    }
}
