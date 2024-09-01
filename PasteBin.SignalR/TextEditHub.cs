using Microsoft.AspNetCore.SignalR;
using PasteBin.Backend.Services.Topics;

namespace PasteBin.SignalR;

public class TextEditHub : Hub, ITextEditHub
{
    public async Task SendEditAsync(string topicId, string userId, string text)
    {
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
