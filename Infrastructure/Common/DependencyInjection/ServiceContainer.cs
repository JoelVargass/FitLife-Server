using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Common.Authentication;
using Infrastructure.Data;
using Infrastructure.Data.Seeders;
using Infrastructure.Persistence;
//using Infrastructure.Services.Assets;
using Infrastructure.Services.Authentication;
using Infrastructure.Services.Email;
using Infrastructure.Services.User;

namespace Infrastructure.Common.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config,
        IHostEnvironment env)
    {
        services.AddJWTAuthenticationScheme(config);

        // Repositorios
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IExercisePlanRepository, ExercisePlanRepository>();
        //services.AddScoped<IRoleRepository, RoleRepository>();

        // Servicios
        services.AddScoped<IJwtGenerator, JwtTokenGenerator>();
        services.AddScoped<IJwtValidator, JwtValidator>();
        services.AddScoped<ITokenService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IUserService, UserService>();

        //services.AddScoped<ImageService>();

        // Configuración para usar solo SQL Server (por defecto en todos los entornos)
        /*
        services.AddDbContext<TutoresParDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("TutoresParConnection"))
               .UseAsyncSeeding(async (context, _, ct) =>
               {
                   await Seeder.AdministrationSeeder.SeedAsync(context);
               })
               .UseSeeding((context, _) =>
               {
                   Seeder.AdministrationSeeder.SeedAsync(context).Wait();
               });
        });
        */

        // Configuración para usar MySQL con Pomelo
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySql(
                    config.GetConnectionString("Connection"),
                    ServerVersion.AutoDetect(config.GetConnectionString("Connection"))
                )
                .UseAsyncSeeding(async (context, _, ct) =>
                {
                    await Seeder.AdministrationSeeder.SeedAsync(context);
                })
                .UseSeeding((context, _) =>
                {
                    Seeder.AdministrationSeeder.SeedAsync(context).Wait();
                });
        });

        return services;
    }
}
