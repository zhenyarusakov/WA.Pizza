using System.Threading.Tasks;
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

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await _authenticateService.Register(model);
        
        return Ok(result);
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var result = await _authenticateService.RegisterAdmin(model);
    
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _authenticateService.Login(model);
    
        return Ok(result);
    }
}