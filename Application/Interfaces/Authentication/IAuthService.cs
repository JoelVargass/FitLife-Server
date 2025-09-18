using ErrorOr;
using Application.Common.Results;

namespace Application.Interfaces.Authentication;

public interface IAuthService
{
    void SetRefreshToken(string token);
    Guid GetUserId();
    bool HasSuperAccess();
}