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
        modelBuilder.Entity<TopicMetadataModel>()
            .HasKey(m => m.TopicId);
        modelBuilder.Entity<TopicMetadataModel>()
            .HasIndex(m => m.ShortUrl)
            .IsUnique();
        modelBuilder.Entity<TopicMetadataModel>()
            .Property(m => m.ShortUrl)
            .HasMaxLength(50);

        base.OnModelCreating(modelBuilder);
    }
}
