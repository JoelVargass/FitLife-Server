using Application.DependencyInjection;
using Infrastructure.Common.DependencyInjection;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Infrastructure.Data;
using WebApi.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Template Api" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    
});

var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Environment)
    .AddPresentation();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// Trigger del seeder
await using (var scope = app.Services.CreateAsyncScope())
await using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}

app.UseCors("DevCorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//  middleware de permisos personalizados
app.UseAuthentication();
//app.UseMiddleware<PermissionAuthorizationMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
