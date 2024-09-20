using PasteBin.Common.Data.KeyValuedDatabases;
using PasteBin.Contracts.Urls;

namespace PasteBin.Services.Urls;
public class UrlProducer(ISetStorageService storageService) : IUrlProducer
{
    private readonly ISetStorageService _storageService = storageService;

    public async Task<bool> CreateAsync(string url, CancellationToken ct)
    {
        return await _storageService.AddAsync(url, ct);
    }

    public async Task<long> GetNumberOfUrlsAsync(CancellationToken ct)
    {
        return await _storageService.GetLengthAsync(ct);
    }
}
