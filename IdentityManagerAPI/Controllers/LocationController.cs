using DataAcess;
using IdentityManager.Services.ControllerService.IControllerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace IdentityManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController(IGeolocationService geoService, ApplicationDbContext context) : ControllerBase
    {
        
        [HttpPost("reverse-geocode")]
        public async Task<IActionResult> GetLocation([FromBody] LocationRequest request)
        {
            string? address = await geoService.GetAddressFromCoordinates(request.Latitude, request.Longitude);
            return Ok(new { Address = address });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAction()
        {
            var locations = await context.Workers
            .Select(w => w!.Location).AsTracking()
            .ToListAsync();

            return Ok(locations);
        }
    }
}
