using PasteBin.Contracts.Topics.Services;
using PasteBin.Contracts.Urls;

namespace PasteBin.Services.Urls;
public class ShortUrlGenerator(
    ITopicMetadataDAO topicMetadataDAO,
    IUrlStorageService urlStorageService) : IShortUrlGenerator
{
    private readonly ITopicMetadataDAO _topicMetadataDAO = topicMetadataDAO;
    private readonly IUrlStorageService _urlStorageService = urlStorageService;

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

            if (await _urlStorageService.AddUrlAsync(url, ct))
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
}
