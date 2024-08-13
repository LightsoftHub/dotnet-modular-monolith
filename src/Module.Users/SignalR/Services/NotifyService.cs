using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.Users.SignalR.Services;

public class NotifyService : INotifyService
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;
    private readonly ILogger<NotifyService> _logger;

    public NotifyService(IHubContext<NotificationHub> notificationHubContext, ILogger<NotifyService> logger) =>
        (_notificationHubContext, _logger) = (notificationHubContext, logger);

    public Task NotifyAsync(CancellationToken cancellationToken = default) =>
        _notificationHubContext.Clients.All
            .SendAsync(NotificationConstants.SERVER_NOTIFICATION, cancellationToken);

    public Task NotifyAsync(string userId, CancellationToken cancellationToken = default) =>
        _notificationHubContext.Clients.User(userId)
            .SendAsync(NotificationConstants.SERVER_NOTIFICATION, cancellationToken);

    public Task NotifyAsync(string[] userIds, CancellationToken cancellationToken = default) =>
        _notificationHubContext.Clients.Users(userIds)
            .SendAsync(NotificationConstants.SERVER_NOTIFICATION, cancellationToken);

    public async Task SendAsync(INotification notification, CancellationToken cancellationToken = default)
    {
        await _notificationHubContext.Clients.All
            .SendAsync(
                NotificationConstants.SERVER_MESSAGE,
                notification.GetType().FullName,
                notification,
                cancellationToken);

        _logger.LogInformation("Send {message} to all users", notification.GetType().FullName);
    }

    public async Task SendAsync(INotification notification, string userId, CancellationToken cancellationToken = default)
    {
        await _notificationHubContext.Clients.User(userId)
            .SendAsync(
                NotificationConstants.SERVER_MESSAGE,
                notification.GetType().FullName,
                notification,
                cancellationToken);

        _logger.LogInformation("Send {message} to user {userId}",
            notification.GetType().FullName, userId);
    }

    public async Task SendAsync(INotification notification, string[] userIds, CancellationToken cancellationToken = default)
    {
        await _notificationHubContext.Clients.Users(userIds)
            .SendAsync(
                NotificationConstants.SERVER_MESSAGE,
                notification.GetType().FullName,
                notification,
                cancellationToken);

        _logger.LogInformation("Send {message} to users {userIds}",
            notification.GetType().FullName, string.Join(",", userIds));
    }
}