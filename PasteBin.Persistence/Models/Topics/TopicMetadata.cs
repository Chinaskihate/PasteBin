using System.ComponentModel.DataAnnotations;

namespace PasteBin.Persistence.Models.Topics;
public class TopicMetadata
{
    [Key]
    public Guid TopicId { get; set; }
    [Required]
    public string ShortUrl { get; set; }
    public string? CreatorId { get; set; }
}
