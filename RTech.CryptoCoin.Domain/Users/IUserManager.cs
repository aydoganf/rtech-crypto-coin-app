namespace RTech.CryptoCoin.Users;

public interface IUserManager
{
    User Create(string username, string password);
}
