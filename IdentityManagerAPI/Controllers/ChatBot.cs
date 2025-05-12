using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using System.Text;

namespace IdentityManagerAPI.Controllers
{
    [ApiController]
    [Route("api/AI")]
    public class ChatBot : ControllerBase
    {
        [HttpGet("ask")]
        public async Task<IActionResult> Get(string prompt , Kernel kernel)
        {
            try
            {
                var resultStream = kernel.InvokePromptStreamingAsync<string>(prompt);
                var fullResult = new StringBuilder();
                await foreach (var part in resultStream)
                {
                    var splitParts = part.Split(["\n", ".", ":", ",", " "], StringSplitOptions.RemoveEmptyEntries);
                    foreach (var splitPart in splitParts)
                    {
                        fullResult.Append(splitPart.Trim() + " ");
                    }
                }
                return Ok(new { response = fullResult.ToString().Trim() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
