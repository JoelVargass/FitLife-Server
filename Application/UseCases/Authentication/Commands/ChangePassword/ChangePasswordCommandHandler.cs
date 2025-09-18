using ErrorOr;
using MediatR;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.Authentication.Common;
using Domain.Common.Errors;

namespace Application.UseCases.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<AuthResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordService passwordService,
        IUserService userService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<ErrorOr<AuthResult>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();

        var user = await _userRepository.IncludeRoleAsync(userId);

        if (user is null)
            return Errors.User.NotFound;
        
        var isCurrentPasswordValid = _passwordService.VerifyPassword(user, command.OldPassword, user.Password!);

        if (!isCurrentPasswordValid)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var newHashedPassword = _passwordService.HashPassword(user, command.NewPassword);
        user.Password = newHashedPassword;

        await _userRepository.UpdateAsync(user);

        var result = await _tokenService.RefreshToken(command.RefreshToken);

        return result;
    }
}