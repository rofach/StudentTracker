using UniversityHistory.Application.Auth;

namespace UniversityHistory.Application.Interfaces.Auth;

public interface IIdentityAccountManager
{
    Task<IdentityAccountDescriptor?> FindByLoginAsync(string login, CancellationToken ct = default);
    Task<IdentityAccountDescriptor?> FindByIdAsync(Guid userId, CancellationToken ct = default);
    Task<IdentityAccountDescriptor?> FindByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<bool> CheckPasswordAsync(Guid userId, string password, CancellationToken ct = default);
    Task EnsureRoleExistsAsync(string roleName, CancellationToken ct = default);
    Task EnsureRoleAssignedAsync(Guid userId, string roleName, CancellationToken ct = default);
    Task EnsureEmailIsAvailableAsync(string email, Guid? ignoredUserId = null, CancellationToken ct = default);
    Task<IdentityAccountDescriptor> CreateAccountAsync(
        string userName,
        string email,
        string password,
        Guid? studentId,
        bool emailConfirmed,
        CancellationToken ct = default,
        bool skipPasswordValidation = false);
    Task UpdateAccountAsync(
        Guid userId,
        string userName,
        string email,
        Guid? studentId,
        bool emailConfirmed,
        CancellationToken ct = default);
    Task SyncEmailAsync(Guid userId, string email, CancellationToken ct = default);
    Task SetPasswordAsync(
        Guid userId,
        string password,
        CancellationToken ct = default,
        bool skipPasswordValidation = false);
}
