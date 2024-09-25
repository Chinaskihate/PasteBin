using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using static PasteBin.Environment.EnvironmentVariables;

namespace PasteBin.Environment.Secrets;
internal class VaultSecretProvider
{
    private readonly string _vaultAddress;
    private readonly string _token;
    private readonly string _env;

    public VaultSecretProvider()
    {
        _vaultAddress = GetEnvVar(VaultAddress);
        _token = GetEnvVar(VaultToken);
        _env = GetEnvVar(AspNetCoreEnvironment);
    }

    public Dictionary<string, string> Load()
    {
        var authMethod = new TokenAuthMethodInfo(_token);

        var vaultClientSettings = new VaultClientSettings(_vaultAddress, authMethod);
        var vaultClient = new VaultClient(vaultClientSettings);

        // Fetch all secrets for the given environment from Vault
        var secrets = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(_env, mountPoint: "secret").Result;

        var result = new Dictionary<string, string>();
        if (secrets != null)
        {
            foreach (var secret in secrets.Data.Data)
            {
                result.Add(secret.Key, secret.Value.ToString());
            }
        }

        return result;
    }
}
