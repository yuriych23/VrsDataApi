using VrsDataApi.Models;

namespace VrsDataApi.DAL.Abstract;

public interface IVrsLogRepository : IRepository
{
    Task<IEnumerable<VrsLogEntry>> GetVrsLogEntriesAsync();
    Task<VrsLogEntry> GetVrsLogEntryAsync(Guid id);
    Task AddVrsLogEntryAsync(VrsLogEntry item);
    Task UpdateVrsLogEntryAsync(VrsLogEntry item);
    Task DeleteVrsLogEntryAsync(Guid id);
}