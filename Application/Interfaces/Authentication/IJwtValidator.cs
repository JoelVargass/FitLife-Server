using ErrorOr;
namespace Application.Interfaces.Authentication;

public interface IJwtValidator
{
    public ErrorOr<Guid> ValidateToken(string token);
}