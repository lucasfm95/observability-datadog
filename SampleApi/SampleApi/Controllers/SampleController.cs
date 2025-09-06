using Datadog.Trace;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController(ILogger<SampleController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        using var scope = Tracer.Instance.StartActive("/SampleApi");
        logger.LogInformation("SampleApi started");
        logger.LogInformation("SampleApi ended");
        return Ok("Hello from SampleApi!");
    }
}