namespace PasteBin.Environment;
internal static class EnvironmentVariables
{
    public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    public const string DevelopmentEnvironment = "Development";
    public const string VaultToken = "VAULT_TOKEN";
    public const string VaultAddress = "VAULT_ADDR";

    public static string GetEnvVar(string name)
    {
        return System.Environment.GetEnvironmentVariable(name)
            ?? throw new ArgumentException($"{name} environment variable not found");
    }

    public static bool IsDevEnv() => GetEnvVar(AspNetCoreEnvironment) == DevelopmentEnvironment;
}
