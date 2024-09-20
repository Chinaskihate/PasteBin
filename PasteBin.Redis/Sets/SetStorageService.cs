using PasteBin.Common.Data.KeyValuedDatabases;
using StackExchange.Redis;

namespace PasteBin.Redis.Sets;
public class SetStorageService(
    IConnectionMultiplexer redis,
    string urlSetName) : ISetStorageService
{
    private readonly IConnectionMultiplexer _redis = redis;
    private readonly string _urlSetName = urlSetName;

    public async Task<bool> AddAsync(string value, CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetAddAsync(_urlSetName, value);
    }

    public async Task<bool> ContainsAsync(string value, CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetContainsAsync(_urlSetName, value);
    }

    public async Task<long> GetLengthAsync(CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetLengthAsync(_urlSetName);
    }

    public async Task<string?> PopAsync(CancellationToken ct)
    {
        var db = _redis.GetDatabase();
        return await db.SetPopAsync(_urlSetName);
    }
}
