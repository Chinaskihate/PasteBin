namespace PasteBin.Contracts.Topics.Services;
public interface ITopicMetadataDAO
{
    Task<TopicMetadata> CreateAsync(TopicMetadata metadata, CancellationToken ct);
    Task<TopicMetadata> GetAsync(string shortUrl, CancellationToken ct);
}
