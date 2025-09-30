using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Commands.Create;

public record CreateUserCommand(
    string Name,
    string FirstLastName,
    string SecondLastName,
    string Email,
    string Password,
    Genre? Genre,
    decimal? Weight,
    decimal? Height
    //Guid RoleId
    ) : IRequest<ErrorOr<Created>>;