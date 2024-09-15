using Light.Identity.EntityFrameworkCore;

namespace ModularMonolith.Modules.Users.Data;

internal class ApplicationDbContext(
    ICurrentUser currentUser,
    IDateTime clock,
    DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    protected override string CurrentUserId => currentUser.UserId ?? base.CurrentUserId;

    protected override bool SoftDelete => false;

    protected override DateTimeOffset Time => clock.Now;

    public virtual DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Notification>().ToTable(name: "Notifications", "System");
    }
}