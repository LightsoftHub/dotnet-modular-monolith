namespace ModularMonolith.WebBlazor.Infrastructure.HttpServices;

public interface ITokenProvider
{
    string? AccessToken { get; }
}
