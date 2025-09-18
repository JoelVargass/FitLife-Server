using Domain.Entities;

namespace Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user, IEnumerable<string> permissions);
}