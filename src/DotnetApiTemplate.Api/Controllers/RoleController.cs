using DotnetApiTemplate.Api.Auth;
using DotnetApiTemplate.Domain.DTO.Role;
using DotnetApiTemplate.Domain.DTO.User;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotnetApiTemplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<RoleWithoutIdDTO>>> GetAllWithoutId()
    {
        return Ok((await _roleService.GetAllWithoutId()).ToList());
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<RoleWithoutIdDTO>>> GetAllWithId()
    {
        return Ok((await _roleService.GetAllWithId()).ToList());
    }
}
