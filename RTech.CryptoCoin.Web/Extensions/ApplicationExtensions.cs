using RTech.CryptoCoin.EntityFrameworkCore;
using RTech.CryptoCoin.Web.Middlewares;
using Microsoft.EntityFrameworkCore;
using RTech.CryptoCoin.Users;
using RTech.CryptoCoin.Web.DbMigrations;
using RTech.CryptoCoin.UnitOfWork;
using RTech.CryptoCoin.EntityFrameworkCore.Users;
using RTech.CryptoCoin.Data;
using RTech.CryptoCoin.Coins;
using RTech.CryptoCoin.EntityFrameworkCore.Coins;
using RTech.CryptoCoin.Web.Workers;

namespace RTech.CryptoCoin.Web.Extensions;

public static class ApplicationExtensions
{
    public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
    {
        app.UseMiddleware<RTechUnitOfWorkMiddleware>();

        return app;
    }

    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<RTechCryptoCoinDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("RTechDb"));
        }, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped);

        builder.Services.AddHttpClient();

        builder.Services.AddSingleton<CryptoCurrencyWorker>();
        builder.Services.AddHostedService(p => p.GetRequiredService<CryptoCurrencyWorker>());

        builder.Services.AddTransient<PendingEfCoreMigrationsChecker>();

        builder.Services.AddScoped<RTechUnitOfWorkMiddleware>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();

        builder.Services.AddScoped<IUserManager, UserManager>();

        builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();
        builder.Services.AddScoped<IBitcoinRepository, EfCoreBitcoinRepository>();

        builder.ConfigureEntityOptions();

        return builder;
    }

    public static WebApplicationBuilder ConfigureEntityOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EntityOptions>(options =>
        {
            options.Entity<User>(e => e.IncludeFunc = query => query.Include(u => u.Logins));
        });

        return builder;
    }
}
