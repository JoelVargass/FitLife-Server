using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Domain.Common.Errors;

namespace Infrastructure.Services.Authentication;

public class JwtValidator : IJwtValidator
{
    private readonly IConfiguration _configuration;
    
    public JwtValidator(IConfiguration configuration, IRepository<Domain.Entities.User> userRepository)
    {
        _configuration = configuration;
    }
    
    public ErrorOr<Guid> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:Key").Value!);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            if (Guid.TryParse(userId, out var userGuid))
                return userGuid;
            else
                return Errors.Authentication.InvalidToken;
            
        }
        catch (SecurityTokenException)
        {
            return Errors.Authentication.InvalidToken;
        }
        catch (Exception)
        {
            return Errors.Authentication.TokenValidationFailed;
        }     
    }
}