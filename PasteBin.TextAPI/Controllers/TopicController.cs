using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBin.Backend.Services.Topics;
using PasteBin.Contracts.Topics.Dto;
using PasteBin.Http.Controllers;

namespace PasteBin.TextAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class TopicController : DefaultController
{
    private readonly ITopicService _topicService;

    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateTopicAsync([FromBody] CreateTopicDto dto)
    {
        var link = await _topicService.CreateTopicAsync(dto);
        return Success(link);
    }
}
