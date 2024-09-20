namespace PasteBin.Contracts.Urls;
public interface IUrlProducer
{
    public Task<bool> CreateAsync(string url, CancellationToken ct);

    public Task<long> GetNumberOfUrlsAsync(CancellationToken ct);
}
