using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.UseCases.Authentication.Common;
using Domain.Common.Errors;
using Domain.Entities;

namespace Application.UseCases.Authentication.Queries.Login;
public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<ErrorOr<AuthResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(query.Email);

        if (user is null)
            return Errors.Authentication.InvalidCredentials;
        
        var passwordHasher = new PasswordHasher<User>();
        var isPasswordValid = passwordHasher.VerifyHashedPassword(user, user.Password!, query.Password);

        if (isPasswordValid == PasswordVerificationResult.Failed)
            return Errors.Authentication.InvalidCredentials;

        var token = await _tokenService.GenerateTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        await _tokenService.StoreRefreshTokenAsync(refreshToken, user.Id);

        return new AuthResult(AccessToken: token, RefreshToken: refreshToken, Email: user.Email,
            Name: user.Name, FirstLastName: user.FirstLastName, SecondLastName: user.SecondLastName);
    }
}