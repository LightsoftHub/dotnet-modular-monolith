using FluentValidation;
using Light.ActiveDirectory;
using Light.ActiveDirectory.Options;
using Light.AspNetCore.Builder;
using Light.AspNetCore.Swagger;
using Light.Identity.EntityFrameworkCore;
using Light.Identity.EntityFrameworkCore.Options;
using ModularMonolith.Auth;
using ModularMonolith.Modules.Users;
using System.Reflection;

namespace ModularMonolith.WebApi;

public static class ConfigureExtensions
{
    private static readonly Assembly[] assemblies =
        [
            typeof(Program).Assembly,
            typeof(UserModule).Assembly,
        ];

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        services.AddValidatorsFromAssemblies(assemblies);

        // Light Framework
        services.AddGlobalExceptionHandler();
        services.AddApiVersion(1);
        services.AddSwagger(configuration, true);
        services.AutoConfigureModuleServices(configuration, assemblies);

        services.AddHttpContextAccessor();
        services.AddCurrentUser();
        services.AddPermissions();
        services.AddAuth(configuration);

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var sectionName = "JWT";

        // Overide by BindConfiguration
        services.AddOptions<JwtOptions>().BindConfiguration(sectionName);

        var domainName = configuration.GetValue<string>("DomainName");
        if (!string.IsNullOrEmpty(domainName))
        {
            services.AddActiveDirectory(x => x.Name = domainName);
        }

        services.AddTokenServices();

        // add JWT Auth
        var jwtSettings = configuration.GetSection(sectionName).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtSettings, nameof(JwtOptions));
        services.AddJwtAuth(jwtSettings.Issuer, jwtSettings.SecretKey); // inject this for use jwt auth

        return services;
    }

    public static IApplicationBuilder ConfigurePipelines(this IApplicationBuilder builder, IConfiguration configuration) =>
    builder
        .UseRequestLoggingMiddleware(configuration)
        .UseExceptionHandler()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseSwagger(configuration, true)
        .AutoConfigureModulePipelines(configuration, assemblies);
}