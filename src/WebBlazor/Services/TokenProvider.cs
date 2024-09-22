using ModularMonolith.Core.Interfaces;
using ModularMonolith.WebBlazor.Infrastructure.HttpServices;

namespace ModularMonolith.WebBlazor.Services;

public class TokenProvider(ICurrentUser currentUser) : ITokenProvider
{
    public string? AccessToken => currentUser.AccessToken;
}