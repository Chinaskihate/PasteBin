namespace PasteBin.Contracts.Topics.Services;
public interface ITextEditHub
{
    public Task SendEditAsync(
        string topicId,
        string userId,
        string text,
        CancellationToken ct);

    public Task JoinTopicAsync(string topicId, CancellationToken ct);

    public Task LeaveTopicAsync(string topicId, CancellationToken ct);
}
