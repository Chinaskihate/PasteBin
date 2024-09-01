namespace PasteBin.Contracts.Topics.Services;
public interface ITopicMetadataDAO
{
    Task<TopicMetadata> Create(TopicMetadata metadata);
}
