using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Services;
using Polly.Registry;

namespace Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private const string FromEmail = "joelvar0914@gmail.com";
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        using var email = new MailMessage(
            new MailAddress(FromEmail, _config["EmailSmtp:Username"]),
            new MailAddress(to))
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        using var smtp = new SmtpClient
        {
            Host = _config["EmailSmtp:Host"]!,
            Port = int.Parse(_config["EmailSmtp:Port"]!),
            Credentials = new NetworkCredential(FromEmail, _config["EmailSmtp:Password"]),
            EnableSsl = true
        };

        await smtp.SendMailAsync(email);
    }
}