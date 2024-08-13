using Light.AspNetCore.Builder;
using Light.AspNetCore.Swagger;
using Light.Extensions.DependencyInjection;
using ModularMonolith.Infrastructure;
using System.Reflection;

namespace ModularMonolith.WebApp;

public static class ProgramExtensions
{
    private static readonly Assembly[] assemblies =
        [
            typeof(Program).Assembly,
        ];

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Light Framework
        services.AddApiVersion(2);
        services.AddSwagger(configuration, true);

        // Libs services
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        // Common Infrastructure
        services.AddInfrastructureServices();

        return services;
    }

    public static IApplicationBuilder ConfigurePipelines(this IApplicationBuilder builder, IConfiguration configuration) =>
    builder
        .UseRequestLoggingMiddleware(configuration)
        .UseExceptionHandlerMiddleware()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseSwagger(configuration, true);
}