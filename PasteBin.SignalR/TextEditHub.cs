using Microsoft.AspNetCore.SignalR;
using PasteBin.Contracts.Topics.Services;

namespace PasteBin.SignalR;

public class TextEditHub(ITopicTextStorageService topicTextStorage) : Hub, ITextEditHub
{
    private readonly ITopicTextStorageService _topicTextStorage = topicTextStorage;

    public async Task SendEditAsync(string topicId, string userId, string text, CancellationToken ct)
    {
        await _topicTextStorage.SaveTextAsync(Guid.Parse(topicId), text, ct);
        await Clients.Group(topicId).SendAsync("ReceiveEdit", userId, text, ct);
    }

    public async Task JoinTopicAsync(string topicId, CancellationToken ct)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, topicId, ct);
    }

    public async Task LeaveTopicAsync(string topicId, CancellationToken ct)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, topicId, ct);
    }
}
