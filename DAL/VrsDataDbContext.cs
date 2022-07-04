using VrsDataApi.Models;
using Microsoft.EntityFrameworkCore;

namespace VrsDataApi.DAL;

public class VrsDataDbContext : DbContext
{
    public VrsDataDbContext(DbContextOptions<VrsDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<VrsLogEntry> VrsLogEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Entries");
    }
}