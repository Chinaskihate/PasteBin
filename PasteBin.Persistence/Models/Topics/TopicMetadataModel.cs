namespace PasteBin.Persistence.Models.Topics;
public class TopicMetadataModel
{
    public Guid TopicId { get; set; }
    public required string ShortUrl { get; set; }
    public Guid CreatorId { get; set; }
}
