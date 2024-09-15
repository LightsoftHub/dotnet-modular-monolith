using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Auth.Permissions;
using ModularMonolith.Core.Interfaces;

namespace ModularMonolith.Auth;

public static class AuthModule
{
    public static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

    public static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services.AddScoped<ICurrentUser, CurrentUser>();
}