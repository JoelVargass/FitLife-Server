using ErrorOr;

namespace WebApi.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddProblemDetails(opt =>
        {
            opt.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                var errors = context.HttpContext.Items["errors"] as List<Error>;

                if (errors is not null)
                    context.ProblemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
            };
        });

        services.AddHttpContextAccessor();

        return services;
    }
    
}
