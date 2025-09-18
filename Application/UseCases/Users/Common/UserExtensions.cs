using Domain.Entities;

namespace Application.UseCases.Users.Common;

public static class UserExtensions
{
    public static UserResult? ToResult(this User user)
    {
        return new UserResult(
            user.Id,
            user.Name,
            user.FirstLastName,
            user.SecondLastName,
            user.Email
            //RoleName: user.Role.Name,
            );
    }
}