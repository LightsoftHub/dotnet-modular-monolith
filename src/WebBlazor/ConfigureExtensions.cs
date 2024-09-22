using Microsoft.AspNetCore.Authentication.Cookies;
using ModularMonolith.Auth;
using ModularMonolith.WebBlazor.Components.Account;
using ModularMonolith.WebBlazor.Components.Spinner;
using ModularMonolith.WebBlazor.Infrastructure;
using ModularMonolith.WebBlazor.Infrastructure.HttpServices;
using ModularMonolith.WebBlazor.Services;

namespace ModularMonolith.WebBlazor;

public static class ServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddCurrentUser();
        services.AddPermissions();

        services.AddScoped<IdentityRedirectManager>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/access-denied");
                //options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = new PathString("/account/login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

        services.AddAuthorization();

        services.AddCascadingAuthenticationState();

        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddHttpApiClients(configuration);
        services.AddHttpApiServices(Web.Assemblies);

        services.AddFluentUIDemoServices();

        services.AddScoped<SpinnerService>();

        return services;
    }

    public static IApplicationBuilder ConfigurePipelines(this IApplicationBuilder builder, IConfiguration configuration) =>
    builder
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseWebSockets();
}
