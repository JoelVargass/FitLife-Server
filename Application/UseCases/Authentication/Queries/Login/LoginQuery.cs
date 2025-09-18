using ErrorOr;
using MediatR;
using Application.UseCases.Authentication.Common;

namespace Application.UseCases.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;