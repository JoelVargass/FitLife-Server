using System.Text;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces.Authentication;

namespace Infrastructure.Services.Authentication;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<Domain.Entities.User> _passwordHasher;

    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<Domain.Entities.User>();
    }

    public string HashPassword(Domain.Entities.User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(Domain.Entities.User user, string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
        
        return result == PasswordVerificationResult.Success;
    }
    
    /**
     * Genera:
     * - 6 caracteres aleatorios
     * - Prefijo fijo "User"
     * - otros 6 caracteres aleatorios
     */
    public string GeneratePassword(string email)
    {
        var randomString = Guid.NewGuid().ToString("N").Substring(0, 6);
        var password = new StringBuilder();

        password.Append("User");
        password.Append(randomString);

        return password.ToString();
    }
}