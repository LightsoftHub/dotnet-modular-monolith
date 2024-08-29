using Microsoft.AspNetCore.Authorization;

namespace ModularMonolith.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string policy) => Policy = policy;
}