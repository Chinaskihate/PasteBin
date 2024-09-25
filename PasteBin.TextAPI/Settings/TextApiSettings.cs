using PasteBin.Environment;
using PasteBin.Persistence;
using PasteBin.Redis;
using PasteBin.S3.Minio.Settings;

namespace PasteBin.TextAPI.Settings;

public class TextApiSettings : ServiceSettings
{
    public int MaxTopicTextLength { get; set; }
    public MinioSettings? Minio { get; set; }
    public string? TextBucketName { get; set; }
    public RedisSettings? Redis { get; set; }

    public string? UrlSetName { get; set; }
    public DbSettings? DbSettings { get; set; }
}
