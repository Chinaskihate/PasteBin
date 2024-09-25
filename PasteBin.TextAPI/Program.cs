using Minio;
using PasteBin.Common.Data.KeyValuedDatabases;
using PasteBin.Common.Data.S3;
using PasteBin.Contracts.Auth;
using PasteBin.Contracts.Text.Validation;
using PasteBin.Contracts.Topics.Services;
using PasteBin.Contracts.Urls;
using PasteBin.Environment;
using PasteBin.Http.Auth;
using PasteBin.Http.Extensions;
using PasteBin.Logging.Extensions;
using PasteBin.Persistence.DataAccess.Topics;
using PasteBin.Persistence.Extensions;
using PasteBin.Persistence.Mappings;
using PasteBin.Redis.Extensions;
using PasteBin.Redis.Sets;
using PasteBin.S3.Extensions;
using PasteBin.S3.Minio;
using PasteBin.Services.Text;
using PasteBin.Services.Topics;
using PasteBin.Services.Urls;
using PasteBin.SignalR;
using PasteBin.TextAPI.Settings;
using StackExchange.Redis;
using PasteBin.Environment.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.InsertVaultSettings();

var serviceSettings = SettingsHelper.GetSettings<TextApiSettings>(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTopicDbContextFactory(serviceSettings.DbSettings.ConnectionString);
builder.Services.AddTopicMapper();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Host.SerilogTo(SerilogOutputType.Console);

builder.Services.AddMinio(serviceSettings.Minio);
builder.Services.AddRedis(serviceSettings.Redis);

builder.Services.AddScoped<ITextValidationService, TextValidationService>(sp =>
    new TextValidationService(serviceSettings.MaxTopicTextLength));
builder.Services.AddScoped<ITopicMetadataDAO, TopicMetadataDAO>();
builder.Services.AddScoped<IS3Client, MinioS3Client>(sp =>
    new MinioS3Client(sp.GetRequiredService<IMinioClientFactory>(),
        serviceSettings.TextBucketName));
builder.Services.AddScoped<ITopicTextStorageService, TopicTextStorageService>();
builder.Services.AddScoped<ISetStorageService, SetStorageService>(sp =>
    new SetStorageService(
            sp.GetRequiredService<IConnectionMultiplexer>(),
            serviceSettings.UrlSetName));
builder.Services.AddScoped<IUrlConsumer, UrlConsumer>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<ITextEditHub, TextEditHubWrapper>();
builder.Services.AddScoped<ITopicService, TopicService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<TextEditHub>("/text-edit-hub");

app.UseGlobalExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
