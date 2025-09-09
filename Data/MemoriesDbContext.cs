using Microsoft.EntityFrameworkCore;


public class MemoriesDbContext : DbContext
{
public MemoriesDbContext(DbContextOptions<MemoriesDbContext> options) : base(options) {}


public DbSet<MemoryFolder> MemoryFolders { get; set; }
public DbSet<MemoryImage> MemoryImages { get; set; }
}