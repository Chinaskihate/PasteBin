namespace PasteBin.Contracts.Topics;
public class TopicMetadata
{
    public Guid TopicId { get; set; }
    public string ShortUrl { get; set; }
    public string? CreatorId { get; set; }
}
