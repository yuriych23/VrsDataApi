using VrsDataApi.Models;
using VrsDataApi.DAL.Abstract;
using Microsoft.EntityFrameworkCore;

namespace VrsDataApi.DAL.Concrete;

public class VrsLogRepository : IVrsLogRepository
{
    private readonly VrsDataDbContext _dbContext;

    public VrsLogRepository(VrsDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddVrsLogEntryAsync(VrsLogEntry item)
    {
        _dbContext.Add(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteVrsLogEntryAsync(Guid id)
    {
        var entry = await _dbContext.VrsLogEntries.FirstOrDefaultAsync(x => x.Id == id);
        if (entry == null)
            return;

       _dbContext.VrsLogEntries.Remove(entry);
       await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<VrsLogEntry>> GetVrsLogEntriesAsync()
    {
        return await _dbContext.VrsLogEntries.ToListAsync();
    }

    public async Task<VrsLogEntry> GetVrsLogEntryAsync(Guid id)
    {
        return await _dbContext.VrsLogEntries.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateVrsLogEntryAsync(VrsLogEntry item)
    {
        var vrsLogEntry = await GetVrsLogEntryAsync(item.Id);

        vrsLogEntry.Position = item.Position;
        vrsLogEntry.Date = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }
}