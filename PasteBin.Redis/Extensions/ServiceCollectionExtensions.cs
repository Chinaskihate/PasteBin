using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace PasteBin.Redis.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services,
        RedisSettings settings) =>
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(settings.ConnectionString));
}