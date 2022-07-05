using Microsoft.AspNetCore.Mvc;
using VrsDataApi.DAL.Abstract;
using Newtonsoft.Json;
using VrsDataApi.Models;

namespace VrsDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VrsLogEntriesController : ControllerBase
{
    private readonly IVrsLogRepository _vrsLogRepository;
    private readonly ILogger<VrsLogEntriesController> _logger;

    public VrsLogEntriesController(ILogger<VrsLogEntriesController> logger, IVrsLogRepository vrsLogRepository)
    {
        _logger = logger;
        _vrsLogRepository = vrsLogRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<VrsLogEntry>> GetVrsLogEntries()
    {
        return await _vrsLogRepository.GetVrsLogEntriesAsync();
    }

    [HttpGet("{id}")]
    public async Task<VrsLogEntry> GetVrsLogEntry(Guid id)
    {
       return await _vrsLogRepository.GetVrsLogEntryAsync(id);
    }

    [HttpPost]
    public async Task<VrsLogEntry> PostVrsLogEntry(VrsLogEntry entry)
    {
        var vrsLogEntry = new VrsLogEntry()
        {
            Id = Guid.NewGuid(),
            ImoNumber = entry.ImoNumber,
            Date = DateTime.UtcNow,
            VesselName = entry.VesselName,
            Position = entry.Position
        };

        await _vrsLogRepository.AddVrsLogEntryAsync(vrsLogEntry);

        return vrsLogEntry;
    }

    [HttpPut("{id}")]
    public async Task<IResult> PutVrsLogEntry(VrsLogEntry entry)
    {
        var vrsLogEntry = await _vrsLogRepository.GetVrsLogEntryAsync(entry.Id);
        if (vrsLogEntry == null)
        {
            return Results.NotFound();
        }

        await _vrsLogRepository.UpdateVrsLogEntryAsync(vrsLogEntry);

        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteVrsLogEntry(Guid id)
    {
        var vrsLogEntry = await _vrsLogRepository.GetVrsLogEntryAsync(id);
        if (vrsLogEntry == null)
        {
            return Results.NotFound();
        }

        await _vrsLogRepository.DeleteVrsLogEntryAsync(id);

        return Results.NoContent();
    }
}
