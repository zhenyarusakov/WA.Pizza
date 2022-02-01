using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Core.Entities.IdentityModels;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Api.Controllers;

public class AuthenticateController: BaseApiController
{
    private readonly IAuthenticateService _authenticateService;
    public AuthenticateController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await _authenticateService.RegisterAsync(model);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var result = await _authenticateService.RegisterAdminAsync(model);
    
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _authenticateService.LoginAsync(model);
    
        return Ok(result);
    }
    
    [HttpPost("addNewRole")]
    public async Task<IActionResult> AddRoleAsync(RoleModel roleModel)
    {
        var result = await _authenticateService.AddRoleAsync(roleModel);
        return Ok(result);
    }
}