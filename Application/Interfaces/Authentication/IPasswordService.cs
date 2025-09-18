using Domain.Entities;

namespace Application.Interfaces.Authentication;

public interface IPasswordService
{
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string password, string hashedPassword);
    //string GeneratePassword(string email);
}