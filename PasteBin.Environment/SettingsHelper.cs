using Microsoft.Extensions.Configuration;

namespace PasteBin.Environment;

public static class SettingsHelper
{
    public static T GetSettings<T>(IConfiguration configuration) where T : ServiceSettings, new()
    {
        var settings = new T();

        configuration.GetSection(typeof(T).Name).Bind(settings);

        return settings;
    }
}
