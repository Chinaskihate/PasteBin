namespace PasteBin.Contracts.Topics.Services;
public interface ITopicMetadataDAO
{
    Task<TopicMetadata> CreateAsync(TopicMetadata metadata, CancellationToken ct);
}
