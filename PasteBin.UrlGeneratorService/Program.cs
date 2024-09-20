using PasteBin.Common.Data.KeyValuedDatabases;
using PasteBin.Contracts.Topics.Services;
using PasteBin.Contracts.Urls;
using PasteBin.Environment;
using PasteBin.Http.Background;
using PasteBin.Logging.Extensions;
using PasteBin.Persistence.DataAccess.Topics;
using PasteBin.Persistence.Extensions;
using PasteBin.Persistence.Helpers;
using PasteBin.Persistence.Mappings;
using PasteBin.Redis.Extensions;
using PasteBin.Redis.Sets;
using PasteBin.Services.Background;
using PasteBin.Services.Urls;
using PasteBin.UrlGeneratorService.Settings;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var serviceSettings = SettingsHelper.GetSettings<UrlGeneratorServiceSettings>(builder.Configuration);

builder.Host.SerilogTo(SerilogOutputType.Console);
builder.Services.AddTopicDbContextFactory(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddTopicMapper();
builder.Services.AddRedis(serviceSettings.Redis);

builder.Services.AddSingleton<ITopicMetadataDAO, TopicMetadataDAO>();
builder.Services.AddSingleton<ISetStorageService, SetStorageService>(sp =>
    new SetStorageService(
            sp.GetRequiredService<IConnectionMultiplexer>(),
            serviceSettings.UrlSetName));
builder.Services.AddSingleton<IUrlProducer, UrlProducer>();
builder.Services.AddSingleton<IShortUrlGenerator, ShortUrlGenerator>();
builder.Services.AddHostedService<DefaultBackgroundService>(sp =>
    new DefaultBackgroundService(
        new UrlGeneratingTask(
            sp.GetRequiredService<IShortUrlGenerator>(),
            serviceSettings.MinUrlCount,
            serviceSettings.DelayOnStop,
            serviceSettings.BatchSize)));

var app = builder.Build();

MigrationHelper.ApplyTopicMigration(app.Services);

app.Run();
