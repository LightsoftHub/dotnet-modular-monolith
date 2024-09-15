using System.Text.Json;

namespace ModularMonolith.HttpApi.Client;

public class HttpClientConstants
{
    public const string BackendApi = nameof(BackendApi);

    public const string IdentityApiVersion = "v1";

    public static JsonSerializerOptions JsonSerializerOptions => new()
    {
        PropertyNameCaseInsensitive = true,
    };
}
