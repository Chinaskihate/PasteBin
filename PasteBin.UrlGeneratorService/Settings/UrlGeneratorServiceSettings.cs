using PasteBin.Environment;
using PasteBin.Persistence;
using PasteBin.Redis;

namespace PasteBin.UrlGeneratorService.Settings;

public class UrlGeneratorServiceSettings : ServiceSettings
{
    public long MinUrlCount { get; set; }
    public TimeSpan DelayOnStop { get; set; }
    public long BatchSize { get; set; }
    public string? UrlSetName { get; set; }
    public RedisSettings? Redis { get; set; }
    public DbSettings? DbSettings { get; set; }
}
