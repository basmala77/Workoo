using IdentityManagerAPI.ControllerService.IControllerService;
using IdentityManagerAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController(IWorkerFacadeService workerFacade, INotificationService notificationService) : Controller
{
    [HttpPost("request-service")]
    public async Task<IActionResult> RequestService([FromBody] ServiceRequest request)
    {
        var result = await workerFacade.HandleServiceRequestWithNotification(request);

        if (!result.isSuccess)
            return BadRequest(result.message);

        return Ok(new ApiResponse { Success = true, Message = "Request sent successfully" });
    }

    [HttpGet("AllService")]
    public async Task<IActionResult> GetAllService()
    {
        var services = await workerFacade.GetAllSpecialties();
        return Ok(services);
    }

    [HttpGet("workers")]
    public async Task<IActionResult> GetNearbyWorker([FromQuery] string category, [FromQuery] double userLat, [FromQuery] double userLon, [FromQuery] string sort)
    {
        var workers = await workerFacade.GetWorkersByCategoryWithNear(category, userLat, userLon, sort);
        return Ok(workers);
    }
}