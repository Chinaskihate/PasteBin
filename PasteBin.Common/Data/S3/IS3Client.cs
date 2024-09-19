namespace PasteBin.Common.Data.S3;
public interface IS3Client
{
    Task SaveAsync(string objectName, byte[] data, CancellationToken ct);

    Task<byte[]> ReadAsync(string objectName, CancellationToken ct);
}
