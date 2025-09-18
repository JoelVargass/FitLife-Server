using Application.Interfaces.Authentication;
using Application.UseCases.Authentication.Commands.AccountRecovery;
using Application.UseCases.Authentication.Commands.ChangePassword;
using Application.UseCases.Authentication.Commands.PasswordReset;
using Application.UseCases.Authentication.Commands.Register;
using Application.UseCases.Authentication.Common;
using Application.UseCases.Authentication.Queries.Login;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;

namespace WebApi.Controllers;

[AllowAnonymous]
public class AuthController(IMediator mediator, IAuthService authService, ITokenService tokenService)
    : ApiController
{
    public record LoginRequest(
        string Email,
        string Password
    );

    public record RegisterRequest(
        string Name,
        string FirstLastName,
        string SecondLastName,
        string Email,
        string Password
    );

    public record PasswordResetRequest(string Token, string Password, string ConfirmPassword);
    
    public record ChangePasswordRequest(string OldPassword, string NewPassword, string ConfirmPassword);

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.Name, request.FirstLastName, request.SecondLastName, request.Email, 
            request.Password);
        var result = await mediator.Send(command);

        return result.Match(authResult =>
        {
            authService.SetRefreshToken(authResult.RefreshToken);
            return Ok(authResult);
        }, Problem);
    }

    [HttpPost("account-recovery")]
    public async Task<IActionResult> AccountRecovery(string email)
    {
        var command = new AccountRecoveryCommand(email);
        ErrorOr<Unit> result = await mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset(PasswordResetRequest request)
    {
        var command = new PasswordResetCommand(request.Token, request.Password, request.ConfirmPassword);
        ErrorOr<AuthResult> authResult = await mediator.Send(command);

        return authResult.Match(result =>
        {
            authService.SetRefreshToken(result.RefreshToken);
            return Ok(result);
        }, Problem);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
            return Problem(Errors.Authentication.MissingRefreshToken);

        var command = new ChangePasswordCommand(request.OldPassword, request.NewPassword, request.ConfirmPassword,
            refreshToken);

        ErrorOr<AuthResult> result = await mediator.Send(command);

        return result.Match(r =>
        {
            authService.SetRefreshToken(r.RefreshToken);
            return Ok(r);
        }, Problem);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        ErrorOr<AuthResult> authResult = await mediator.Send(query);

        return authResult.Match(
            response =>
            {
                authService.SetRefreshToken(response.RefreshToken);
                return Ok(response);
            },
            Problem);
    }

    [HttpPost("refresh-token/{refreshToken}")]
    public async Task<IActionResult> Token( string refreshToken)
    {
        if (refreshToken is null || string.IsNullOrEmpty(refreshToken))
            return Problem(Errors.Authentication.MissingRefreshToken);

        if (!await tokenService.ValidateRefreshTokenAsync(refreshToken))
            return Problem(Errors.Authentication.InvalidRefreshToken);

        var result = await tokenService.RefreshToken(refreshToken);

        return result.Match(authResult =>
        {
            authService.SetRefreshToken(authResult.RefreshToken);
            return Ok(authResult);
        }, Problem);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
            return Problem(Errors.Authentication.MissingRefreshToken);

        await tokenService.DeleteRefreshTokenAsync(refreshToken);
        return Ok();
    }

}