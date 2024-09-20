using PasteBin.Common.Exceptions;
using PasteBin.Contracts.Auth;
using PasteBin.Contracts.Exceptions;
using PasteBin.Contracts.Text.Validation;
using PasteBin.Contracts.Topics;
using PasteBin.Contracts.Topics.Dto;
using PasteBin.Contracts.Topics.Services;
using PasteBin.Contracts.Urls;
using PasteBin.Resources.Errors;
using System.Transactions;

namespace PasteBin.Services.Topics;
public class TopicService(
    ITextValidationService validationService,
    ITopicMetadataDAO topicMetadataDAO,
    ITopicTextStorageService topicTextStorage,
    IUrlConsumer urlConsumer,
    IUserContextService userContextService,
    ITextEditHub editHub) : ITopicService
{
    private readonly ITextValidationService _validationService = validationService;
    private readonly ITopicMetadataDAO _topicMetadataDAO = topicMetadataDAO;
    private readonly ITopicTextStorageService _topicTextStorage = topicTextStorage;
    private readonly IUrlConsumer _urlConsumer = urlConsumer;
    private readonly IUserContextService _userContextService = userContextService;
    private readonly ITextEditHub _editHub = editHub;

    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto dto, CancellationToken ct)
    {
        _validationService.ValidateText(dto.Text);

        var topicMetadata = new TopicMetadata
        {
            TopicId = Guid.NewGuid(),
            ShortUrl = await _urlConsumer.PopUrlAsync(ct)
                ?? throw new NoAvailableUrlException(),
            CreatorId = _userContextService.UserId,
        };

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await _topicMetadataDAO.CreateAsync(topicMetadata, ct);
        await _topicTextStorage.SaveTextAsync(topicMetadata.TopicId, dto.Text!, ct);
        scope.Complete();

        await _editHub.SendEditAsync(
            topicMetadata.TopicId.ToString(),
            _userContextService.UserId,
            dto.Text,
            ct);

        return new TopicResponseDto
        {
            TopicId = topicMetadata.TopicId,
            ShortUrl = topicMetadata.ShortUrl,
            Text = dto.Text,
        };
    }

    public async Task<TopicResponseDto> GetTopicAsync(string shortUrl, CancellationToken ct)
    {
        var result = await _topicMetadataDAO.GetAsync(shortUrl, ct)
            ?? throw new NotFoundException(string.Format(Errors.TopicNotFound, shortUrl));
        var text = await _topicTextStorage.GetTextAsync(result.TopicId, ct);

        return new TopicResponseDto
        {
            TopicId = result.TopicId,
            ShortUrl = result.ShortUrl,
            Text = text,
        };
    }
}
