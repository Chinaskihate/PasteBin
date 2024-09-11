using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasteBin.Persistence.DbContexts;

namespace PasteBin.Persistence.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTopicDbContextFactory(this IServiceCollection services, string connectionString) =>
        services.AddDbContextFactory<TopicDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
}
