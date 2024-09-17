using Minio;
using Minio.DataModel.Args;
using PasteBin.Common.S3;

namespace PasteBin.S3.Minio;
public class MinioS3Client(IMinioClientFactory factory, string bucketName) : IS3Client
{
    private readonly IMinioClientFactory _factory = factory;
    private readonly string _bucketName = bucketName;

    public async Task SaveAsync(string objectName, byte[] data, CancellationToken ct)
    {
        using var client = _factory.CreateClient();
        var bucketFound = await client.BucketExistsAsync(new BucketExistsArgs()
            .WithBucket(_bucketName), ct);
        if (!bucketFound)
        {
            await client.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }

        using var contentStream = new MemoryStream(data);
        await client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(contentStream)
            .WithObjectSize(contentStream.Length)
            .WithContentType("text/plain"), ct);
    }

    public async Task<byte[]> ReadAsync(string objectName, CancellationToken ct)
    {
        byte[] result = Array.Empty<byte>();

        using var client = _factory.CreateClient();
        await client.GetObjectAsync(new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithCallbackStream(stream =>
            {
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }), ct);

        return result;
    }
}
