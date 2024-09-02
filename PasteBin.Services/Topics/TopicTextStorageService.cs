using PasteBin.Contracts.S3;
using PasteBin.Contracts.Topics.Services;
using System.Text;

namespace PasteBin.Services.Topics;
public class TopicTextStorageService(IS3Client s3Client) : ITopicTextStorageService
{
    private readonly IS3Client _s3Client = s3Client;

    public async Task<string> GetTextAsync(Guid topicId, CancellationToken ct)
    {
        var data = await _s3Client.ReadAsync(topicId.ToString(), ct);
        return Encoding.Default.GetString(data);
    }

    public async Task SaveTextAsync(Guid topicId, string text, CancellationToken ct)
    {
        var bytes = Encoding.Default.GetBytes(text);
        await _s3Client.SaveAsync(topicId.ToString(), bytes, ct);
    }
}
