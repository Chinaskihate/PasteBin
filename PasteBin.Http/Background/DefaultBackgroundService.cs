using Microsoft.Extensions.Hosting;
using PasteBin.Common.Background;

namespace PasteBin.Http.Background;
public class DefaultBackgroundService(IBackgroundTask backgroundTask) : BackgroundService
{
    private readonly IBackgroundTask _backgroundTask = backgroundTask;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _backgroundTask.ExecuteAsync(stoppingToken);
    }
}
