using Light.ActiveDirectory.Interfaces;
using Light.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Modules.Users.Endpoints;

[MustHavePermission(Permissions.Users.View)]
public class UserController(
    IUserService userService,
    IActiveDirectoryService activeDirectoryService) : VersionedApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var res = await userService.GetAllAsync();
        return res.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        var res = await userService.GetByIdAsync(id);
        return res.ToActionResult();
    }

    [HttpGet("by_username/{username}")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        var res = await userService.GetByUserNameAsync(username);
        return res.ToActionResult();
    }

    [HttpPost]
    [MustHavePermission(Permissions.Users.Create)]
    public async Task<IActionResult> PostAsync(CreateUserRequest request)
    {
        var res = await userService.CreateAsync(request);
        return res.ToActionResult();
    }

    [HttpPut("{id}")]
    [MustHavePermission(Permissions.Users.Update)]
    public async Task<IActionResult> PutAsync(string id, UserDto request)
    {
        if (id != request.Id)
        {
            return Result.Error("Validate User ID not match").ToActionResult();
        }

        var res = await userService.UpdateAsync(request);
        return res.ToActionResult();
    }

    [HttpDelete("{id}")]
    [MustHavePermission(Permissions.Users.Delete)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var res = await userService.DeleteAsync(id);
        return res.ToActionResult();
    }

    [HttpPut("{id}/force_password")]
    [MustHavePermission(Permissions.Users.Update)]
    public async Task<IActionResult> ForcePasswordAsync(string id, [FromBody] string password)
    {
        var res = await userService.ForcePasswordAsync(id, password);
        return res.ToActionResult();
    }

    [HttpGet("get_domain_user/{userName}")]
    public async Task<IActionResult> GetDomainUserAsync([FromRoute] string userName)
    {
        var res = await activeDirectoryService.GetByUserNameAsync(userName);
        return res.ToActionResult();
    }
}
