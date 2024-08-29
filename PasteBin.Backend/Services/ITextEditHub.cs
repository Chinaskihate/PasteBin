namespace PasteBin.Backend.Services;
public interface ITextEditHub
{
    public Task SendEditAsync(string topicId, string userId, string text);

    public Task JoinTopicAsync(string topicId);

    public Task LeaveTopicAsync(string topicId);
}
