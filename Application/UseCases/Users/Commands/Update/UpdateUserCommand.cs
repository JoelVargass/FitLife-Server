using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Commands.Update;

public record UpdateUserCommand(
    Guid Id,
    string Name,
    string FirstLastName,
    string SecondLastName,
    string Email,
    string Password,
    Genre? Genre,
    decimal? Weight,
    decimal? Height
    //Guid RoleId
    ) : IRequest<ErrorOr<Updated>>;