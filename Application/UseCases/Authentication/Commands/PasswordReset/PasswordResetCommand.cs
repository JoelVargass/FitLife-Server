using ErrorOr;
using MediatR;
using Application.UseCases.Authentication.Common;

namespace Application.UseCases.Authentication.Commands.PasswordReset;

public record PasswordResetCommand(string RecoveryToken, string Password, string ConfirmPassword) : IRequest<ErrorOr<AuthResult>>;