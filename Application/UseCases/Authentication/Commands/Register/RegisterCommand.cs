using ErrorOr;
using MediatR;
using Application.UseCases.Authentication.Common;
using Domain.Entities;

namespace Application.UseCases.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string FirstLastName,
    string SecondLastName,
    string Email,
    string Password,
    Genre? Genre,
    decimal? Weight,
    decimal? Height
    ) : IRequest<ErrorOr<AuthResult>>;