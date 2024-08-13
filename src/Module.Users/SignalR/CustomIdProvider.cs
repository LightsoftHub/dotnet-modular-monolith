using Light.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ModularMonolith.Users.SignalR;

public class CustomIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        var userId = connection.User?.FindFirst(ClaimTypes.UserId)?.Value;
        return userId;
    }
}