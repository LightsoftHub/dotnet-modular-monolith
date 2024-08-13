using Microsoft.EntityFrameworkCore;

namespace ModularMonolith.Users.SignalR.UseCases;

public record CountUnreadUserNotification(string UserId) : IQuery<Result<long>>;

internal class CountUnreadUserNotificationHandler(
    AppIdentityDbContext context) : IQueryHandler<CountUnreadUserNotification, Result<long>>
{
    public async Task<Result<long>> Handle(CountUnreadUserNotification request, CancellationToken cancellationToken)
    {
        var count = await context.Notifications
            .Where(x => x.ToUserId == request.UserId && x.MarkAsRead == false)
            .CountAsync(cancellationToken);

        return Result<long>.Success(count);
    }
}
