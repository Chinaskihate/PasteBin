using Microsoft.EntityFrameworkCore;

namespace PasteBin.Persistence.DbContexts;
public class TopicDbContext : DbContext
{
    public TopicDbContext()
    {
        
    }

    public TopicDbContext(DbContextOptions<TopicDbContext> options) : base(options)
    {
        
    }
}
