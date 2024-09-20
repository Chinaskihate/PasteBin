using PasteBin.Common.Background;
using PasteBin.Contracts.Urls;

namespace PasteBin.Services.Background;
public class UrlGeneratingTask(
    IShortUrlGenerator urlGenerator,
    long minUrlCount,
    TimeSpan delayOnStop,
    long batchSize) : IBackgroundTask
{
    private readonly IShortUrlGenerator _urlGenerator = urlGenerator;
    private readonly long _minUrlCount = minUrlCount;
    private readonly TimeSpan _delayOnStop = delayOnStop;
    private readonly long _batchSize = batchSize;

    public async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var currentUrlCount = await _urlGenerator.GetNumberOfGeneratedUrls(ct);
            if (currentUrlCount - _minUrlCount < 0)
            {
                for (int i = 0; i < _batchSize; i++)
                {
                    await _urlGenerator.GenerateShortUrlAsync(ct);
                }
            }

            await Task.Delay(_delayOnStop, ct);
        }
    }
}
