using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasteBin.Persistence.DbContexts;

namespace PasteBin.Persistence.Helpers;
public class MigrationHelper
{
    public static void ApplyTopicMigration(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        using var db = scope.ServiceProvider
            .GetRequiredService<IDbContextFactory<TopicDbContext>>()
            .CreateDbContext();
        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}
