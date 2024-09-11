using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PasteBin.Contracts.Topics;
using PasteBin.Persistence.Models.Topics;

namespace PasteBin.Persistence.Mappings;
public static class TopicMappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<TopicMetadata, TopicMetadataModel>()
                .ReverseMap();
        });

        return mappingConfig;
    }

    public static IServiceCollection AddTopicMapper(this IServiceCollection services)
    {
        var mapper = RegisterMaps().CreateMapper();
        return services.AddSingleton(mapper);
    }
}