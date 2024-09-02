namespace PasteBin.Contracts.Topics;
public class TopicMetadata
{
    public Guid TopicId { get; set; }
    public required string ShortUrl { get; set; }
    public string? CreatorId { get; set; }
}
