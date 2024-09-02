namespace PasteBin.Contracts.Topics.Services;
public interface ITopicTextStorageService
{
    Task SaveTextAsync(Guid topicId, string text, CancellationToken ct);

    Task<string> GetTextAsync(Guid topicId, CancellationToken ct);
}
