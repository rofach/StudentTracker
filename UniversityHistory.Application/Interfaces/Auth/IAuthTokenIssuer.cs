using UniversityHistory.Application.Auth;

namespace UniversityHistory.Application.Interfaces.Auth;

public interface IAuthTokenIssuer
{
    IssuedAccessToken IssueAccessToken(IdentityAccountDescriptor account);
}
