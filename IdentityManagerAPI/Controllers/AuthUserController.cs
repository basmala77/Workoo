
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Auth;
using IdentityManager.Services.ControllerService.IControllerService;

namespace IdentityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController(IAuthService authService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
        {
            var result = await authService.LoginAsync(loginRequestDto);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto)
        {
            var result = await authService.RegisterAsync(registerRequestDto);
            return Ok(result);
        }
    }
}
