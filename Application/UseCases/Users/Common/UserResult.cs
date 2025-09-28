using Domain.Entities;

namespace Application.UseCases.Users.Common;

public record UserResult(
    Guid Id,
    string Name,
    string FirstLastName,
    string? SecondLastName,
    string Email,
    Genre? Genre,
    decimal? Weight,
    decimal? Height
    //string RoleName,
    );