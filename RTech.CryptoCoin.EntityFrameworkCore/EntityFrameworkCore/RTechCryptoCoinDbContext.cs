using Microsoft.EntityFrameworkCore;
using RTech.CryptoCoin.Coins;
using RTech.CryptoCoin.Users;

namespace RTech.CryptoCoin.EntityFrameworkCore;

public class RTechCryptoCoinDbContext : DbContext
{
    public RTechCryptoCoinDbContext(DbContextOptions<RTechCryptoCoinDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bitcoin>(b =>
        {
            b.Property(e => e.Id).ValueGeneratedNever();
            b.Property(e => e.Value).IsRequired();

            b.ToTable(nameof(Bitcoin));
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<User>(b =>
        {
            b.Property(e => e.Id).ValueGeneratedNever();
            b.Property(e => e.Username).IsRequired().HasMaxLength(UserConstants.UsernameMaxLength);
            b.Property(e => e.Password).IsRequired().HasMaxLength(UserConstants.PasswordMaxLength);
            b.Property(e => e.Salt).IsRequired().HasMaxLength(UserConstants.SaltMaxLength);

            b.HasMany(e => e.Logins).WithOne().HasForeignKey(e => e.UserId);

            b.ToTable(nameof(User));
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<Login>(b =>
        {
            b.Property(e => e.Id).ValueGeneratedNever();
            b.Property(e => e.IpAddress).IsRequired().HasMaxLength(LoginConstants.IpAddressMaxLength);

            b.ToTable(nameof(Login));
            b.ConfigureByConvention();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql(Configuration.GetConnectionString("RTechDb"));
    }
}