using System.ComponentModel.DataAnnotations;

namespace PasteBin.Persistence.Models.Topics;
public class TopicMetadataModel
{
    [Key]
    public Guid TopicId { get; set; }
    [Required]
    public required string ShortUrl { get; set; }
    public string? CreatorId { get; set; }
}
