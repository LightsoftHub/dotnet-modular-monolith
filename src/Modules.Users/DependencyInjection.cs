using Light.AspNetCore.Modules;
using Light.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Users.Data;

namespace ModularMonolith.Modules.Users;

public record UserModule;

public class UserModuleServices : ModuleServiceCollection
{
    public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var useMemoryDb = configuration.GetValue<bool>("UseInMemoryDatabase");
        if (useMemoryDb)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("IdentityDb"));
        }
        else
        {
            var connectionStr = configuration.GetConnectionString("UserConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStr));
        }

        services.AddIdentity<ApplicationDbContext>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;

            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            // Lockout settings
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
            //options.Lockout.MaxFailedAccessAttempts = 10;

            // User settings
            options.User.RequireUniqueEmail = false;
        });

        return services;
    }
}