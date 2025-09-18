using ErrorOr;
using MediatR;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.UseCases.Authentication.Common;
using Domain.Common.Errors;

namespace Application.UseCases.Authentication.Commands.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, ErrorOr<AuthResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public PasswordResetCommandHandler(IUserRepository userRepository,
        IPasswordService passwordService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<ErrorOr<AuthResult>> Handle(PasswordResetCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRecoveryTokenAsync(command.RecoveryToken);

        if (user is null)
            return Errors.Authentication.InvalidToken;

        // Verificar que la nueva contraseña y su confirmación coincidan
        if (command.Password != command.ConfirmPassword)
            return Errors.Authentication.InvalidCredentials;

        // Actualizar la contraseña del usuario
        user.Password = _passwordService.HashPassword(user, command.Password);
        user.RecoveryToken = null;

        await _userRepository.UpdateAsync(user);

        var token = await _tokenService.GenerateTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        await _tokenService.StoreRefreshTokenAsync(refreshToken, user.Id);

        return new AuthResult(AccessToken: token, RefreshToken: refreshToken, Email: user.Email,
            Name: user.Name,
            FirstLastName: user.FirstLastName,
            SecondLastName: user.SecondLastName);
    }
}