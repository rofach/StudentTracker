using System.Security.Claims;

namespace UniversityHistory.API.Auth;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var rawValue = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue("sub");

        return Guid.TryParse(rawValue, out var userId) ? userId : null;
    }

    public static Guid? GetStudentId(this ClaimsPrincipal user)
    {
        var rawValue = user.FindFirstValue(AuthClaimTypes.StudentId);
        return Guid.TryParse(rawValue, out var studentId) ? studentId : null;
    }
}
