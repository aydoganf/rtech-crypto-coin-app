using Microsoft.EntityFrameworkCore;
using RTech.CryptoCoin.EntityFrameworkCore;

namespace RTech.CryptoCoin.Web.DbMigrations;

public class PendingEfCoreMigrationsChecker
{
    private readonly IServiceProvider _serviceProvider;

    public PendingEfCoreMigrationsChecker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task CheckAndApplyDatabaseMigrationsAsync()
    {
        var dbContext = _serviceProvider.GetRequiredService<RTechCryptoCoinDbContext>();

        var pendingMigrations = await dbContext
            .Database
            .GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}
