namespace PasteBin.Common.S3;
public interface IS3Client
{
    Task SaveAsync(string objectName, byte[] data, CancellationToken ct);

    Task<byte[]> ReadAsync(string objectName, CancellationToken ct);
}
