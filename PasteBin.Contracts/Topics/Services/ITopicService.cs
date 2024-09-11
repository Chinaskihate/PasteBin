﻿using PasteBin.Contracts.Topics.Dto;

namespace PasteBin.Contracts.Topics.Services;
public interface ITopicService
{
    Task<string> CreateTopicAsync(CreateTopicDto dto, CancellationToken ct);
    Task<string> GetTopicAsync(string shortUrl, CancellationToken ct);
}
