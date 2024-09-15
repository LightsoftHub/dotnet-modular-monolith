using Light.ActiveDirectory.Interfaces;
using Light.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Modules.Users.Endpoints;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/oauth")]
public class TokenController(
    IUserService userService,
    IActiveDirectoryService adService,
    ITokenService tokenService) : VersionedApiController
{
    private readonly IUserService _userService = userService;
    private readonly IActiveDirectoryService _adService = adService;
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost("get_token")]
    public async Task<IActionResult> GetTokenAsync([FromBody] GetTokenRequest request)
    {
        var user = await _userService.GetByUserNameAsync(request.UserName);
        if (user.Succeeded is false)
            return BadRequest(user);

        if (user.Data.UseDomainPassword)
        {
            var domainLogin = await _adService.CheckPasswordSignInAsync(request.UserName, request.Password);

            if (domainLogin.Succeeded is false)
                return BadRequest(domainLogin);
        }
        else
        {
            var localLogin = await _userService.CheckPasswordByUserNameAsync(request.UserName, request.Password);

            if (localLogin.Succeeded is false)
                return BadRequest(localLogin);
        }

        var token = await _tokenService.GetTokenByUserNameAsync(request.UserName);

        return token.ToActionResult();
    }

    [HttpPost("refresh_token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        var token = await _tokenService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

        return token.ToActionResult();
    }
}