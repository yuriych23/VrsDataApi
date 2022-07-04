using Microsoft.AspNetCore.Mvc;
using VrsDataApi.DAL.Abstract;
using Newtonsoft.Json;

namespace VrsDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VrsLogController : ControllerBase
{
    private readonly IVrsLogRepository _vrsLogRepository;
    private readonly ILogger<HelloController> _logger;

    public VrsLogController(ILogger<HelloController> logger, IVrsLogRepository vrsLogRepository)
    {
        _logger = logger;
        _vrsLogRepository = vrsLogRepository;
    }

    [HttpGet]
    public async Task<string> Get()
    {
        // var entry = new VrsLogEntry()
        // {
        //     Id = Guid.NewGuid(),
        //     ImoNumber = 1234567,
        //     Date = DateTime.UtcNow,
        //     VesselName = "DreamMary",
        //     Position = new Position
        //     {
        //         Latitude = 23.234235,
        //         Longitude = 45.9823
        //     }
        // };

        var entry = await _vrsLogRepository.GetVrsLogEntriesAsync();

        return JsonConvert.SerializeObject(entry);
    }

    [HttpGet("{id}")]
    public async Task<string> Get(Guid id)
    {
        var entry = await _vrsLogRepository.GetVrsLogEntryAsync(id);

        return JsonConvert.SerializeObject(entry);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _vrsLogRepository.DeleteVrsLogEntryAsync(id);
    }
}
