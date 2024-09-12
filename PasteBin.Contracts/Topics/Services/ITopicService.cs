using PasteBin.Contracts.Topics.Dto;

namespace PasteBin.Contracts.Topics.Services;
public interface ITopicService
{
    Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto dto, CancellationToken ct);
    Task<TopicResponseDto> GetTopicAsync(string shortUrl, CancellationToken ct);
}
