using Microsoft.Extensions.Configuration;
using PasteBin.Environment.Secrets;
using System.Reflection;

namespace PasteBin.Environment.Extensions;
public static class ConfigurationExtensions
{
    public static IConfiguration InsertVaultSettings(this IConfiguration config)
    {
        if (!EnvironmentVariables.IsDevEnv())
        {
            var baseType = typeof(ServiceSettings);
            var settingsType = Assembly.GetCallingAssembly()
                .GetTypes()
                .First(t => t != baseType && baseType.IsAssignableFrom(t));
            var props = settingsType.GetProperties();
            var secrets = new VaultSecretProvider()
                .Load();
            foreach (var prop in props)
            {
                InsertVaultSettingsInProperty(prop, settingsType.Name, config, secrets);
            }
        }

        return config;
    }

    private static void InsertVaultSettingsInProperty(
        PropertyInfo currentProp,
        string currentPath,
        IConfiguration config,
        Dictionary<string, string> secrets)
    {
        var configPath = $"{currentPath}:{currentProp.Name}";
        if (!currentProp.PropertyType.IsClass || currentProp.PropertyType == typeof(string))
        {
            var currentValue = config[configPath];
            if (currentValue[0] == '{' && currentValue[^1] == '}')
            {
                config[configPath] = secrets[currentValue[1..^1]];
            }

            return;
        }

        var props = currentProp.PropertyType.GetProperties();
        foreach (var prop in props)
        {
            InsertVaultSettingsInProperty(prop, configPath, config, secrets);
        }
    }
}
