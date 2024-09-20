using PasteBin.Common.Data.KeyValuedDatabases;
using PasteBin.Contracts.Urls;

namespace PasteBin.Services.Urls;
public class UrlConsumer(ISetStorageService storageService) : IUrlConsumer
{
    private readonly ISetStorageService _storageService = storageService;

    public async Task<string?> PopUrlAsync(CancellationToken ct)
    {
        return await _storageService.PopAsync(ct);
    }
}
