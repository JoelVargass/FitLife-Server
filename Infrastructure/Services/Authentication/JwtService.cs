using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Results;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.UseCases.Authentication.Common;
//using Domain.Common.Constants;
using Domain.Common.Errors;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services.Authentication;

public class JwtService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;
    //private readonly IPermissionRepository _permissionRepository;
/*
    public JwtService(IRoleRepository roleRepository, IConfiguration config, AppDbContext context,
        IPermissionRepository permissionRepository)
    {
        _config = config;
        _context = context;
        _permissionRepository = permissionRepository;
    }
    */

    public async Task<string> GenerateTokenAsync(Domain.Entities.User user)
    {
        
        //var permissions = await _permissionRepository.GetByRoleAsync(user.RoleId);

        var claims = new List<Claim>
        {
            new("jti", Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new("id", user.Id.ToString()),
            //new("roleName", user.Role.Name),
            //new(Constants.PermissionsClaim,
            //    string.Join(",", permissions.Select(p => new FormattedPermission(p).ToString())))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Authentication:Key")!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Authentication:JwtExpireMinutes")),
            Issuer = _config.GetValue<string>("Authentication:Issuer")!,
            Audience = _config.GetValue<string>("Authentication:Audience")!,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var existToken = await _context.RefreshTokens.Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken);

        return existToken is not null && existToken.Expires > DateTime.UtcNow;
    }

    public async Task StoreRefreshTokenAsync(string refreshToken, Guid userId)
    {
        var existingToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (existingToken is not null)
        {
            // Actualizar el token existente
            existingToken.Token = refreshToken;
            existingToken.Expires =
                DateTime.UtcNow.AddDays(_config.GetValue<int>("Authentication:RefreshTokenExpireDays"));

            _context.RefreshTokens.Update(existingToken);
        }
        else
        {
            // Insertar un nuevo token si no existe
            var entity = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(_config.GetValue<int>("Authentication:RefreshTokenExpireDays"))
            };

            await _context.RefreshTokens.AddAsync(entity);
        }

        await _context.SaveChangesAsync();
    }


    public async Task<ErrorOr<AuthResult>> RefreshToken(string refreshToken)
    {
        var existToken = _context.RefreshTokens.Include(r => r.User)
            //.ThenInclude(u => u.Role)
            .FirstOrDefault(r => r.Token == refreshToken);

        if (existToken is null || existToken.Expires < DateTime.UtcNow)
            return Errors.Authentication.InvalidRefreshToken;

        existToken.Token = GenerateRefreshToken();
        existToken.Expires = DateTime.UtcNow.AddDays(_config.GetValue<int>("Authentication:RefreshTokenExpireDays"));

        _context.RefreshTokens.Update(existToken);
        await _context.SaveChangesAsync();

        var token = await GenerateTokenAsync(existToken.User);
        var user = existToken.User;

        return new AuthResult(AccessToken: token, RefreshToken: existToken.Token, Email: user.Email,
            Name: user.Name, FirstLastName: user.FirstLastName,
            SecondLastName: user.SecondLastName);
    }

    public async Task DeleteRefreshTokenAsync(string refreshToken)
    {
        var existToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);

        if (existToken is not null)
        {
            _context.RefreshTokens.Remove(existToken);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<string?> GetRefreshTokenAsync(Guid userId)
    {
        var token = await _context.RefreshTokens
            .Where(r => r.UserId == userId)
            .Select(r => r.Token)
            .FirstOrDefaultAsync();

        return token;
    }
}