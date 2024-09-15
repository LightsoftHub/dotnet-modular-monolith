using ModularMonolith.Core.Interfaces;
using ModularMonolith.WebComponents;

namespace ModularMonolith.WebBlazor.Services;

public class TokenProvider(ICurrentUser currentUser) : ITokenProvider
{
    public string? AccessToken => currentUser.AccessToken;
}