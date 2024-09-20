namespace PasteBin.Contracts.Urls;
public interface IShortUrlGenerator
{
    Task<string> GenerateShortUrlAsync(CancellationToken ct);

    Task<long> GetNumberOfGeneratedUrls(CancellationToken ct);
}
