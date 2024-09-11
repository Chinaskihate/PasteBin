using PasteBin.Contracts.Auth;
using PasteBin.Contracts.Text.Validation;
using PasteBin.Contracts.Topics;
using PasteBin.Contracts.Topics.Dto;
using PasteBin.Contracts.Topics.Services;
using System.Transactions;

namespace PasteBin.Services.Topics;
public class TopicService(
    ITextValidationService validationService,
    ITopicMetadataDAO topicMetadataDAO,
    ITopicTextStorageService topicTextStorage,
    IUserContextService userContextService,
    ITextEditHub editHub) : ITopicService
{
    private readonly ITextValidationService _validationService = validationService;
    private readonly ITopicMetadataDAO _topicMetadataDAO = topicMetadataDAO;
    private readonly ITopicTextStorageService _topicTextStorage = topicTextStorage;
    private readonly IUserContextService _userContextService = userContextService;
    private readonly ITextEditHub _editHub = editHub;

    public async Task<string> CreateTopicAsync(CreateTopicDto dto, CancellationToken ct)
    {
        _validationService.ValidateText(dto.Text);

        var topicMetadata = new TopicMetadata
        {
            TopicId = Guid.NewGuid(),
            ShortUrl = Guid.NewGuid().ToString(),
            CreatorId = _userContextService.UserId,
        };

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await _topicMetadataDAO.CreateAsync(topicMetadata, ct);
        await _topicTextStorage.SaveTextAsync(topicMetadata.TopicId, dto.Text!, ct);
        scope.Complete();

        await _editHub.SendEditAsync(
            topicMetadata.TopicId.ToString(),
            _userContextService.UserId,
            dto.Text);

        return result.ShortUrl;
    }

    public async Task<string> GetTopicAsync(string shortUrl, CancellationToken ct)
    {
        var result = await _topicMetadataDAO.GetAsync(shortUrl, ct);
        var text = await _topicTextStorage.GetTextAsync(result.TopicId, ct);

        return text;
    }
}
