using DataAcess;
using IdentityManagerAPI.ControllerService.IControllerService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController(IWorkerFacadeService workerService) : ControllerBase
{
    [HttpGet("By_category")]
    public async Task<IActionResult> GetWorkersByCategory(string category)
    {
        var workers = await workerService.GetWorkersByCategory(category);
        return workers.Any() ? Ok(workers) : NotFound("No worker found in the category");
    }

    [HttpGet("Top_Worker")]
    public async Task<IActionResult> GetTopRatedWorkers(int count)
    {
        var workers = await workerService.GetTopRatedWorkers(count);
        return Ok(workers);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddWorker([FromBody] Worker? worker)
    {
        if (worker == null)
            return BadRequest("Invalid data.");

        var added = await workerService.AddWorker(worker);
        return Ok(added);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorker(int id)
    {
        var worker = await workerService.GetWorkerById(id);
        return worker != null ? Ok(worker) : NotFound();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllWorkers()
    {
        var workers = await workerService.GetAllWorkers();
        return Ok(workers);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateWorker(int id, [FromBody] Worker updatedWorker)
    {
        var worker = await workerService.UpdateWorker(id, updatedWorker);
        return worker != null ? Ok(worker) : NotFound();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteWorker(int id)
    {
        var success = await workerService.DeleteWorker(id);

        return success ? Ok("Worker deleted successfully.") : NotFound();
    }
}