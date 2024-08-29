using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Auth.Permissions;
using ModularMonolith.Shared.Interfaces;

namespace ModularMonolith.Auth;

public static class Startup
{
    public static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

    public static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services.AddScoped<ICurrentUser, CurrentUser>();
}