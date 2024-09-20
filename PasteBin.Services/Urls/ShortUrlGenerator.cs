using PasteBin.Contracts.Topics.Services;
using PasteBin.Contracts.Urls;

namespace PasteBin.Services.Urls;
public class ShortUrlGenerator(
    ITopicMetadataDAO topicMetadataDAO,
    IUrlProducer urlProducer) : IShortUrlGenerator
{
    private readonly ITopicMetadataDAO _topicMetadataDAO = topicMetadataDAO;
    private readonly IUrlProducer _urlProducer = urlProducer;

    public async Task<string> GenerateShortUrlAsync(CancellationToken ct)
    {
        string url;
        do
        {
            url = Generate();

            var metadata = await _topicMetadataDAO.GetAsync(url, ct);
            if (metadata != null)
            {
                continue;
            }

            if (await _urlProducer.CreateAsync(url, ct))
            {
                break;
            }
        } while (true);

        return url;
    }

    private static string Generate()
    {
        var bytes = Guid.NewGuid().ToByteArray();
        var base64 = Convert.ToBase64String(bytes)
                          .Replace("+", "-")
                          .Replace("/", "_")
                          .TrimEnd('=');
        return base64[..8];
    }

    public async Task<long> GetNumberOfGeneratedUrls(CancellationToken ct)
    {
        return await _urlProducer.GetNumberOfUrlsAsync(ct);
    }
}
