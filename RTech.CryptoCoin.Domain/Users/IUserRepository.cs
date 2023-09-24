using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> FindAsync(string username, string password);
    Task<User> FindAsync(string username);
}
