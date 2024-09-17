using PasteBin.Contracts.Urls;
using StackExchange.Redis;

namespace PasteBin.Redis.Urls;
public class UrlStorageService(
    IConnectionMultiplexer redis,
    string urlSetName) : IUrlStorageService
{
    private readonly IConnectionMultiplexer _redis = redis;
    private readonly string _urlSetName = urlSetName;

    public async Task<bool> AddUrlAsync(string url, CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetAddAsync(_urlSetName, url);
    }

    public async Task<bool> ContainsUrlAsync(string url, CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetContainsAsync(_urlSetName, url);
    }

    public async Task<string?> PopUrlAsync(CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetPopAsync(_urlSetName);
    }
}
