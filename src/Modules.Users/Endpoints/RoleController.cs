using Light.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Modules.Users.Endpoints;

[MustHavePermission(Permissions.Roles.View)]
public class RoleController(IRoleService roleService) : VersionedApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        return Ok(await roleService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] string id)
    {
        var result = await roleService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Roles.Create)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
    {
        var res = await roleService.CreateAsync(request);
        return res.ToActionResult();
    }

    [HttpPut]
    [Authorize(Policy = Permissions.Roles.Update)]
    public async Task<IActionResult> UpdateAsync([FromBody] RoleDto request)
    {
        var res = await roleService.UpdateAsync(request);
        return res.ToActionResult();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Roles.Delete)]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        return Ok(await roleService.DeleteAsync(id));
    }
}
