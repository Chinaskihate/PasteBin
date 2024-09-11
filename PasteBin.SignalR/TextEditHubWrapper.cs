using Microsoft.AspNetCore.SignalR;
using PasteBin.Contracts.Topics.Services;

namespace PasteBin.SignalR;
public class TextEditHubWrapper(IHubContext<TextEditHub> hubContext) : ITextEditHub
{
    private readonly IHubContext<TextEditHub> _hubContext = hubContext;

    public async Task SendEditAsync(string topicId, string userId, string text)
    {
        await _hubContext.Clients.Group(topicId).SendAsync("ReceiveEdit", userId, text);
    }

    public async Task JoinTopicAsync(string topicId)
    {
        await _hubContext.Groups.AddToGroupAsync(topicId, topicId);
    }

    public async Task LeaveTopicAsync(string topicId)
    {
        await _hubContext.Groups.RemoveFromGroupAsync(topicId, topicId);
    }
}
