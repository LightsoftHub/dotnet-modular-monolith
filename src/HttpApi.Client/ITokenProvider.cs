namespace ModularMonolith.HttpApi.Client;

public interface ITokenProvider
{
    string? AccessToken { get; }
}
