namespace PasteBin.Contracts.Urls;
public interface IUrlConsumer
{
    Task<string?> PopUrlAsync(CancellationToken ct);
}
