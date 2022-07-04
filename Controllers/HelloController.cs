using Microsoft.AspNetCore.Mvc;

namespace VrsDataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    private readonly ILogger<HelloController> _logger;

    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<string> Get(int id)
    {
        await Task.Delay(1);
        return $"Hello {id}";
    }
}
