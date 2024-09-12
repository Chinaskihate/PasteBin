using Microsoft.AspNetCore.SignalR;
using PasteBin.Contracts.Topics.Services;

namespace PasteBin.SignalR;

public class TextEditHub(ITopicTextStorageService topicTextStorage) : Hub, ITextEditHub
{
    private readonly ITopicTextStorageService _topicTextStorage = topicTextStorage;

    public async Task SendEditAsync(string topicId, string userId, string text)
    {
        await _topicTextStorage.SaveTextAsync(Guid.Parse(topicId), text, CancellationToken.None);
        await Clients.Group(topicId).SendAsync("ReceiveEdit", userId, text);
    }

    public async Task JoinTopicAsync(string topicId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, topicId);
    }

    public async Task LeaveTopicAsync(string topicId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, topicId);
    }
}
