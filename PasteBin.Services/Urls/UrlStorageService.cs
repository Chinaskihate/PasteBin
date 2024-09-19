using PasteBin.Common.Data.KeyValuedDatabases;
using PasteBin.Contracts.Urls;

namespace PasteBin.Services.Urls;
public class UrlStorageService(ISetStorageService storageService) : IUrlStorageService
{
    private readonly ISetStorageService _storageService = storageService;

    public async Task<bool> AddUrlAsync(string url, CancellationToken ct)
    {
        return await _storageService.AddAsync(url, ct);
    }

    public async Task<bool> ContainsUrlAsync(string url, CancellationToken ct)
    {
        return await _storageService.ContainsAsync(url, ct);
    }

    public async Task<string?> PopUrlAsync(CancellationToken ct)
    {
        return await _storageService.PopAsync(ct);
    }
}
