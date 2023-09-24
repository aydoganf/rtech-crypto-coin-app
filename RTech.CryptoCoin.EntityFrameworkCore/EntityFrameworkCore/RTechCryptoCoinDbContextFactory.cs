using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RTech.CryptoCoin.EntityFrameworkCore;

public class RTechCryptoCoinDbContextFactory : IDesignTimeDbContextFactory<RTechCryptoCoinDbContext>
{
    private readonly string _connectionString;

    public RTechCryptoCoinDbContextFactory()
    {
        _connectionString = GetConnectionStringFromConfiguration();
    }

    public RTechCryptoCoinDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<RTechCryptoCoinDbContext>()
            .UseNpgsql(_connectionString, b =>
            {
                b.MigrationsHistoryTable("__RTechDb_Migrations");
            });

        return new RTechCryptoCoinDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString("RTechDb");
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}RTech.CryptoCoin.Web"
                )
            )
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
