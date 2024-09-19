namespace PasteBin.Common.Background;
public interface IBackgroundTask
{
    Task ExecuteAsync(CancellationToken ct);
}
