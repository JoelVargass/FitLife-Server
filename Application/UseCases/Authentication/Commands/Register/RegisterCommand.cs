using ErrorOr;
using MediatR;
using Application.UseCases.Authentication.Common;

namespace Application.UseCases.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string FirstLastName,
    string SecondLastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;