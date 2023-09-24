using RTech.CryptoCoin.Coins;
using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin.EntityFrameworkCore.Coins;

public class EfCoreBitcoinRepository : EfCoreRepository<RTechCryptoCoinDbContext, Bitcoin, Guid>, IBitcoinRepository
{
    public EfCoreBitcoinRepository(RTechCryptoCoinDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
    }
}
