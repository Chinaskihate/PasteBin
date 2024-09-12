using Microsoft.AspNetCore.SignalR;
using PasteBin.Contracts.Topics.Services;

namespace PasteBin.SignalR;
public class TextEditHubWrapper(IHubContext<TextEditHub> hubContext) : ITextEditHub
{
    private readonly IHubContext<TextEditHub> _hubContext = hubContext;

    public async Task SendEditAsync(string topicId, string userId, string text, CancellationToken ct)
    {
        await _hubContext.Clients.Group(topicId).SendAsync("ReceiveEdit", userId, text, ct);
    }

    public async Task JoinTopicAsync(string topicId, CancellationToken ct)
    {
        await _hubContext.Groups.AddToGroupAsync(topicId, topicId, ct);
    }

    public async Task LeaveTopicAsync(string topicId, CancellationToken ct)
    {
        await _hubContext.Groups.RemoveFromGroupAsync(topicId, topicId, ct);
    }
}
