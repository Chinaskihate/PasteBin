namespace PasteBin.Contracts.Topics.Dto;
public class TopicResponseDto
{
    public Guid TopicId { get; set; }
    public required string Text { get; set; }
    public required string ShortUrl { get; set; }
}
