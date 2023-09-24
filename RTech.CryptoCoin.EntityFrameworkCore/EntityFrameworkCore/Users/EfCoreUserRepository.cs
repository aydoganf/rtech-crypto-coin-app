using RTech.CryptoCoin.Data;
using RTech.CryptoCoin.Users;

namespace RTech.CryptoCoin.EntityFrameworkCore.Users;

public class EfCoreUserRepository : EfCoreRepository<RTechCryptoCoinDbContext, User, Guid>, IUserRepository
{
    public EfCoreUserRepository(RTechCryptoCoinDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
    }

    public async Task<User> FindAsync(string username, string password)
    {
        return await FindAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<User> FindAsync(string username)
    {
        return await FindAsync(u => u.Username == username);
    }
}
