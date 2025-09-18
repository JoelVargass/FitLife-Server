using ErrorOr;
using MediatR;

namespace Application.UseCases.Authentication.Commands.AccountRecovery;

public record AccountRecoveryCommand(string Email) : IRequest<ErrorOr<Unit>>;