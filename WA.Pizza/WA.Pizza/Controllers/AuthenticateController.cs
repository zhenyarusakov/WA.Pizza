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
        _authenticateService.SetRefreshTokenCookie(result.RefreshToken);
        
        return Ok(result);
    }
    
    [HttpPost("addNewRole")]
    public async Task<IActionResult> AddRoleAsync(AddToRoleModel addToRoleModel)
    {
        await _authenticateService.AddToRoleAsync(addToRoleModel);
        
        return Ok();
    }

    [HttpPost("RegenerateAccessToken")]
    public async Task<IActionResult> RegenerateAccessToken()
    {
        var token = Request.Cookies["refreshToken"];

        if(string.IsNullOrEmpty(token))
            return BadRequest("No refresh token cookie found.");

        var response = await _authenticateService.RegenerateAccessTokenAsync(token);
        _authenticateService.SetRefreshTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    [HttpPost("RevokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest revokeTokenRequest)
    {
        var tokenToRevoke = revokeTokenRequest.Token ?? Request.Cookies["refreshToken"];

        if(tokenToRevoke == null)
            return BadRequest(new { message = "No refresh tokens received." });
        
        await _authenticateService.RevokeToken(tokenToRevoke);
    
        return Ok();
    }
    
}