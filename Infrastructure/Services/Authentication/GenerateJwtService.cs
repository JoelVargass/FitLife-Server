using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces.Authentication;

namespace Infrastructure.Services.Authentication;

public class JwtTokenGenerator : IJwtGenerator
{
    private readonly IConfiguration _config;

    public JwtTokenGenerator(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public string GenerateToken(Domain.Entities.User user, IEnumerable<string> permissions)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (permissions is null) throw new ArgumentNullException(nameof(permissions));

        var key = _config["Authentication:Key"];
        var issuer = _config["Authentication:Issuer"];
        var audience = _config["Authentication:Audience"];
        
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
        {
            throw new InvalidOperationException("JWT configuration is missing required values.");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            //new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim("id", user.Id.ToString()),
            //new Claim("permissions", string.Join(",", permissions.Distinct()))
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
