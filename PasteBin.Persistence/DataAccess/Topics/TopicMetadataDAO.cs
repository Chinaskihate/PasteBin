using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PasteBin.Contracts.Topics;
using PasteBin.Contracts.Topics.Services;
using PasteBin.Persistence.DbContexts;
using PasteBin.Persistence.Models.Topics;

namespace PasteBin.Persistence.DataAccess.Topics;
public class TopicMetadataDAO(
    IDbContextFactory<TopicDbContext> dbContextFactory,
    IMapper mapper) : ITopicMetadataDAO
{
    private readonly IDbContextFactory<TopicDbContext> _dbContextFactory = dbContextFactory;
    private readonly IMapper _mapper = mapper;

    public async Task<TopicMetadata> CreateAsync(TopicMetadata metadata, CancellationToken ct)
    {
        var model = _mapper.Map<TopicMetadataModel>(metadata);
        using var context = _dbContextFactory.CreateDbContext();
        context.TopicMetadatas.Add(model);

        await context.SaveChangesAsync(ct);

        return metadata;
    }

    public async Task<TopicMetadata?> GetAsync(string shortUrl, CancellationToken ct)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var metadata = await context.TopicMetadatas.FirstOrDefaultAsync(m => m.ShortUrl == shortUrl, cancellationToken: ct);

        return metadata == null ? null : _mapper.Map<TopicMetadata>(metadata);
    }
}
