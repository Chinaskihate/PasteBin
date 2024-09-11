using Microsoft.Extensions.DependencyInjection;
using Minio;
using PasteBin.S3.Minio.Settings;

namespace PasteBin.S3.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinio(this IServiceCollection services, MinioSettings settings) =>
        services.AddMinio(client => client
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .WithRegion(settings.Region)
            .WithSSL(false)
            .Build());
}
