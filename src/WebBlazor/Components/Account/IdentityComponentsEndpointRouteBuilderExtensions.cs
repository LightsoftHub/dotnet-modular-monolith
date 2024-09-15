using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ModularMonolith.WebBlazor.Components.Account
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/account");

            accountGroup.MapPost("/logout", async (
                HttpContext httpContext,
                ClaimsPrincipal user,
                [FromForm] string returnUrl) =>
            {
                foreach (var cookie in httpContext.Request.Cookies.Keys)
                {
                    httpContext.Response.Cookies.Delete(cookie);
                }

                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return TypedResults.LocalRedirect($"/account/login");
                }

                return TypedResults.LocalRedirect($"~/{returnUrl}");
            });

            return accountGroup;
        }
    }
}
