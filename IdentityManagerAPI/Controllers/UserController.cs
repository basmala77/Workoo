using DataAcess.Repos.IRepos;
using IdentityManager.Services.ControllerService.IControllerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.image;
using System.Security.Claims;

namespace IdentityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IUserRepository userRepo) : ControllerBase
    {

        public IUserRepository UserRepo { get; } = userRepo;

        [HttpPost]
        [Authorize]
        [Route("uploadUserImage")]
        public async Task<IActionResult> UploadUserImage([FromForm] ImageUploadRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await userService.UploadUserImageAsync(userId, request);

            return Ok(result);
        }
    
    }
}
