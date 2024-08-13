namespace ModularMonolith.Users.SignalR.Services;

public interface INotifyService
{
    /// <summary>
    /// Notify user has new notification
    /// </summary>
    Task NotifyAsync(CancellationToken cancellationToken = default);
    Task NotifyAsync(string userId, CancellationToken cancellationToken = default);
    Task NotifyAsync(string[] userIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send message to users
    /// </summary>
    Task SendAsync(INotification notification, CancellationToken cancellationToken = default);
    Task SendAsync(INotification notification, string userId, CancellationToken cancellationToken = default);
    Task SendAsync(INotification notification, string[] userIds, CancellationToken cancellationToken = default);
}