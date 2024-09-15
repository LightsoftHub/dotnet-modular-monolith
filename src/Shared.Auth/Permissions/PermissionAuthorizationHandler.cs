using Microsoft.AspNetCore.Authorization;
using ModularMonolith.Core.Interfaces;

namespace ModularMonolith.Auth.Permissions;

internal class PermissionAuthorizationHandler(ICurrentUser currentUser) :
    AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (currentUser.HasPermission(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        await Task.CompletedTask;
    }
}