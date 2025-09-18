using ErrorOr;
using MediatR;
using Application.UseCases.Authentication.Common;

namespace Application.UseCases.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand(string OldPassword, string NewPassword, string ConfirmPassword, string RefreshToken) : IRequest<ErrorOr<AuthResult>>;