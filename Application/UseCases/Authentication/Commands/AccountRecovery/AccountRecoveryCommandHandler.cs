using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Common.Errors;

namespace Application.UseCases.Authentication.Commands.AccountRecovery;

public class AccountRecoveryCommandHandler : IRequestHandler<AccountRecoveryCommand, ErrorOr<Unit>>
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AccountRecoveryCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IConfiguration config)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _config = config;
    }

    public async Task<ErrorOr<Unit>> Handle(AccountRecoveryCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);

        if (user is null)
            return Errors.User.NotFound;

        if (user.Password is null)
            return Errors.User.ExternalAuthenticationConflict;

        var recoveryToken = GenerateRecoveryToken();

        user.RecoveryToken = recoveryToken;

        await _userRepository.UpdateAsync(user);

        var templatePath = Path.Combine(AppContext.BaseDirectory, "Common", "Templates", "AccountRecoveryTemplate.html");
        var template = await File.ReadAllTextAsync(templatePath, cancellationToken);

        var recoveryLink = $"/recovery-account?token={recoveryToken}";
        template = template.Replace("{{recoveryToken}}", recoveryLink);

        await _emailService.SendAsync(
            to: user.Email,
            subject: "Recuperación de cuenta",
            body: template);

        return Unit.Value;
    }

    private string GenerateRecoveryToken()
    {
        return Guid.NewGuid().ToString();
    }
}
