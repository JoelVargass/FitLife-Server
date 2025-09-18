using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Interfaces.Services;
//using Domain.Common.Constants;

namespace Infrastructure.Services.User;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst("id")?.Value;
        return userId != null ? Guid.Parse(userId) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }
    

    /*
    public string GetUserRole()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }
*/
    /*
    public bool HasPermission(string permission)
    {
        var permissions = _httpContextAccessor.HttpContext?.User.FindFirst(TutoresParConstants.PermissionsClaim)?.Value;
        if (permissions is null) return false;

        var list = permissions.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return list.Contains(permission, StringComparer.OrdinalIgnoreCase);
    }

    public bool HasSuperAccess()
    {
        var roleClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        
        return roleClaim == TutoresParConstants.AdminRole;
    }
    
    
    public IEnumerable<string> GetPermissions()
    {
        var permissionsClaim = _httpContextAccessor.HttpContext?.User
            .Claims.FirstOrDefault(c => c.Type == TutoresParConstants.PermissionsClaim)?.Value;

        if (string.IsNullOrEmpty(permissionsClaim))
            return Enumerable.Empty<string>();

        return permissionsClaim.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }
*/
}