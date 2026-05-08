using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.Auth;
using UniversityHistory.Application.Interfaces.Auth;
using UniversityHistory.Domain.Exceptions;

namespace UniversityHistory.Infrastructure.Identity;

public class IdentityAccountManager : IIdentityAccountManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public IdentityAccountManager(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityAccountDescriptor?> FindByLoginAsync(string login, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(login)
            ?? await _userManager.FindByEmailAsync(login);

        return user is null ? null : await MapAsync(user);
    }

    public async Task<IdentityAccountDescriptor?> FindByIdAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user is null ? null : await MapAsync(user);
    }

    public async Task<IdentityAccountDescriptor?> FindByStudentIdAsync(Guid studentId, CancellationToken ct = default)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(candidate => candidate.StudentId == studentId, ct);
        return user is null ? null : await MapAsync(user);
    }

    public async Task<bool> CheckPasswordAsync(Guid userId, string password, CancellationToken ct = default)
    {
        var user = await GetRequiredUserAsync(userId);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task EnsureRoleExistsAsync(string roleName, CancellationToken ct = default)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            return;

        var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        EnsureIdentitySuccess(result, $"Не вдалося створити роль {roleName}.");
    }

    public async Task EnsureRoleAssignedAsync(Guid userId, string roleName, CancellationToken ct = default)
    {
        var user = await GetRequiredUserAsync(userId);
        if (await _userManager.IsInRoleAsync(user, roleName))
            return;

        var result = await _userManager.AddToRoleAsync(user, roleName);
        EnsureIdentitySuccess(result, $"Не вдалося призначити роль {roleName} користувачу.");
    }

    public async Task EnsureEmailIsAvailableAsync(string email, Guid? ignoredUserId = null, CancellationToken ct = default)
    {
        var normalizedEmail = _userManager.NormalizeEmail(email);
        var normalizedUserName = _userManager.NormalizeName(email);

        var existingUser = await _userManager.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user =>
                (!ignoredUserId.HasValue || user.Id != ignoredUserId.Value)
                && (user.NormalizedEmail == normalizedEmail || user.NormalizedUserName == normalizedUserName), ct);

        if (existingUser is not null)
            throw new DomainException("Користувач з таким email уже існує.");
    }

    public async Task<IdentityAccountDescriptor> CreateAccountAsync(
        string userName,
        string email,
        string password,
        Guid? studentId,
        bool emailConfirmed,
        CancellationToken ct = default)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = email,
            EmailConfirmed = emailConfirmed,
            StudentId = studentId,
        };

        var result = await _userManager.CreateAsync(user, password);
        EnsureIdentitySuccess(result, "Не вдалося створити обліковий запис.");

        return await MapAsync(user);
    }

    public async Task UpdateAccountAsync(
        Guid userId,
        string userName,
        string email,
        Guid? studentId,
        bool emailConfirmed,
        CancellationToken ct = default)
    {
        var user = await GetRequiredUserAsync(userId);

        user.UserName = userName;
        user.NormalizedUserName = _userManager.NormalizeName(userName);
        user.Email = email;
        user.NormalizedEmail = _userManager.NormalizeEmail(email);
        user.StudentId = studentId;
        user.EmailConfirmed = emailConfirmed;

        var result = await _userManager.UpdateAsync(user);
        EnsureIdentitySuccess(result, "Не вдалося оновити обліковий запис.");
    }

    public async Task SyncEmailAsync(Guid userId, string email, CancellationToken ct = default)
    {
        var user = await GetRequiredUserAsync(userId);

        if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase)
            && string.Equals(user.UserName, email, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var conflictingUser = await _userManager.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate =>
                candidate.Id != user.Id
                && (candidate.StudentId == user.StudentId
                    || candidate.NormalizedEmail == _userManager.NormalizeEmail(email)
                    || candidate.NormalizedUserName == _userManager.NormalizeName(email)), ct);

        if (conflictingUser is not null)
            throw new DomainException("Неможливо оновити email, бо такий логін уже використовується іншим акаунтом.");

        user.Email = email;
        user.NormalizedEmail = _userManager.NormalizeEmail(email);
        user.UserName = email;
        user.NormalizedUserName = _userManager.NormalizeName(email);
        user.EmailConfirmed = true;

        var result = await _userManager.UpdateAsync(user);
        EnsureIdentitySuccess(result, "Не вдалося синхронізувати email облікового запису.");
    }

    public async Task SetPasswordAsync(Guid userId, string password, CancellationToken ct = default)
    {
        var user = await GetRequiredUserAsync(userId);

        IdentityResult result;
        if (await _userManager.HasPasswordAsync(user))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            result = await _userManager.ResetPasswordAsync(user, token, password);
        }
        else
        {
            result = await _userManager.AddPasswordAsync(user, password);
        }

        EnsureIdentitySuccess(result, "Не вдалося оновити пароль користувача.");
    }

    private async Task<ApplicationUser> GetRequiredUserAsync(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);
    }

    private async Task<IdentityAccountDescriptor> MapAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return new IdentityAccountDescriptor(
            user.Id,
            user.UserName ?? user.Email ?? user.Id.ToString(),
            user.Email,
            user.StudentId,
            roles.ToArray());
    }

    private static void EnsureIdentitySuccess(IdentityResult result, string message)
    {
        if (result.Succeeded)
            return;

        var errors = string.Join("; ", result.Errors.Select(error => error.Description));
        throw new DomainException($"{message} {errors}");
    }
}
