using System.Security.Authentication;
using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Application.Common.Results;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
//using Domain.Common.Constants;
using Domain.Common.Errors;
using Domain.Entities;

namespace Infrastructure.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    //private readonly IPermissionRepository _permissionRepository;

    public AuthService(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration config,
        IUserRepository userRepository
        //IPermissionRepository permissionRepository
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _config = config;
        _userRepository = userRepository;
        //_permissionRepository = permissionRepository;
    }

    public void SetRefreshToken(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(_config.GetValue<int>("Authentication:RefreshTokenExpireDays")),
            SameSite = SameSiteMode.None,
            Secure = true
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    public Guid GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var claim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return Guid.TryParse(claim, out var userId)
            ? userId
            : throw new AuthenticationException("Usuario no autenticado");
    }

    public bool HasSuperAccess()
    {
        // Eliminado porque ya no existe el permiso ni se necesita
        throw new NotImplementedException("Este método ya no es válido porque 'superAccess' ha sido eliminado.");
    }
    
}
