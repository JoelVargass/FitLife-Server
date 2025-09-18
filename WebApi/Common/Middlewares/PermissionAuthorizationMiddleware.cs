using WebApi.Common.Attributes;

//using Domain.Common.Constants;

namespace WebApi.Common.Middlewares;

public class PermissionAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public PermissionAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint is null)
        {
            await _next(context);
            return;
        }

        var permissionAttribute = endpoint.Metadata.GetMetadata<RequiredPermissionAttribute>();

        if (permissionAttribute is null)
        {
            await _next(context);
            return;
        }

        var requiredPermission = permissionAttribute.Permission;
        var user = context.User;

        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("No autenticado.");
            return;
        }

        //var permissionsClaim = user.FindFirst(c => c.Type == TutoresParConstants.PermissionsClaim)?.Value;
/*
        if (string.IsNullOrWhiteSpace(permissionsClaim))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("No tienes permisos.");
            return;
        }

        var permissions = new HashSet<string>(
            permissionsClaim.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
            StringComparer.OrdinalIgnoreCase);

        if (!permissions.Contains(requiredPermission))
        {
            
            //Console.WriteLine($"Permisos del usuario: {string.Join(", ", permissions)}");
            //Console.WriteLine($"Permiso requerido: {requiredPermission}");
            
            
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync($"No tienes el permiso requerido: {requiredPermission}");
            return;
        }
*/

        await _next(context);
    }
}
