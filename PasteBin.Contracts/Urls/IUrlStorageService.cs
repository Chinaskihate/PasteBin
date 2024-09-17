namespace PasteBin.Contracts.Urls;
public interface IUrlStorageService
{
    Task<string?> PopUrlAsync(CancellationToken ct);

    Task<bool> ContainsUrlAsync(string url, CancellationToken ct);

    Task<bool> AddUrlAsync(string url, CancellationToken ct);
}
