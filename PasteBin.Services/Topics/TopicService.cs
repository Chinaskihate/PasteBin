using PasteBin.Contracts.Auth;
using PasteBin.Contracts.Text.Validation;
using PasteBin.Contracts.Topics.Dto;
using PasteBin.Contracts.Topics.Services;

namespace PasteBin.Services.Topics;
public class TopicService(
    ITextValidationService validationService,
    IUserContextService userContextService) : ITopicService
{
    private readonly ITextValidationService _validationService = validationService;
    private readonly IUserContextService _userContextService = userContextService;

    public Task<string> CreateTopicAsync(CreateTopicDto dto)
    {
        _validationService.ValidateText(dto.Text);


    }
}
