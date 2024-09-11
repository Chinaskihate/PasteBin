﻿using PasteBin.Environment;
using PasteBin.S3.Minio.Settings;

namespace PasteBin.TextAPI.Settings;

public class TextApiSettings : ServiceSettings
{
    public int MaxTopicTextLength { get; set; }
    public MinioSettings? Minio { get; set; }
    public string? TextBucketName { get; set; }
}
