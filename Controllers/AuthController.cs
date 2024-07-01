using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    public async Task<IActionResult> RefreshToken
    (
        [FromBody] RefreshTokenRequest request
    )
    {
        return Ok(await _authService.RefreshToken(request));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login
    (
        [FromBody] LoginRequest request
    )
    {
        return Ok(await _authService.LoginUser(request));
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request
    )
    {
        await _authService.RegisterUser(request);
        return Created();
    }
    
}