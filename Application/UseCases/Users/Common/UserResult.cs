namespace Application.UseCases.Users.Common;

public record UserResult(
    Guid Id,
    string Name,
    string FirstLastName,
    string? SecondLastName,
    string Email
    //string RoleName,
    );