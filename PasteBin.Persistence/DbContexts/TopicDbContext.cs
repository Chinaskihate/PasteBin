using Microsoft.EntityFrameworkCore;
using PasteBin.Persistence.Models.Topics;

namespace PasteBin.Persistence.DbContexts;
public class TopicDbContext : DbContext
{
    public TopicDbContext() : base()
    {
        
    }

    public TopicDbContext(DbContextOptions<TopicDbContext> options) : base(options)
    {
        
    }

    public DbSet<TopicMetadataModel> TopicMetadatas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
