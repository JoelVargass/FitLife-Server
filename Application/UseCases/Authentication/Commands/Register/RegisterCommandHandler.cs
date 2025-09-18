using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.UseCases.Authentication.Common;
//using Domain.Common.Constants;
using Domain.Common.Errors;
using Domain.Entities;

namespace Application.UseCases.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
{
    private readonly IUserRepository _userRepository;
    //private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        //IRoleRepository roleRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        //_roleRepository = roleRepository;
        _tokenService = tokenService;
    }

    public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Para evitar problemas con el case sensitive de los emails
        var email = command.Email.ToLower();

        var user = await _userRepository.GetByEmailAsync(email);

        if (user is not null)
            return Errors.User.DuplicateEmail;
/*
        var devRole = await _roleRepository.GetByNameAsync(TutoresParConstants.StudentRole);

        if (devRole is null)
            return Errors.Role.NotFound;
*/
        var passwordHasher = new PasswordHasher<User>();

        var newUser = new User
        {
            Name = command.Name,
            FirstLastName = command.FirstLastName,
            SecondLastName = command.SecondLastName,
            Email = email,
            Password = command.Password,
            //RoleId = devRole.Id
        };

        newUser.Password = passwordHasher.HashPassword(newUser, newUser.Password);
        await _userRepository.InsertAsync(newUser);

        var token = await _tokenService.GenerateTokenAsync(newUser);
        var refreshToken = _tokenService.GenerateRefreshToken();
        await _tokenService.StoreRefreshTokenAsync(refreshToken, newUser.Id);

        return new AuthResult(
            AccessToken: token,
            RefreshToken: refreshToken,
            Email: newUser.Email,
            Name: newUser.Name,
            FirstLastName: newUser.FirstLastName,
            SecondLastName: newUser.SecondLastName
        );
    }
}