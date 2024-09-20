namespace PasteBin.Common.Data.KeyValuedDatabases;
public interface ISetStorageService
{
    public Task<bool> AddAsync(string value, CancellationToken ct);

    public Task<string?> PopAsync(CancellationToken ct);

    public Task<bool> ContainsAsync(string value, CancellationToken ct);

    public Task<long> GetLengthAsync(CancellationToken ct);
}
