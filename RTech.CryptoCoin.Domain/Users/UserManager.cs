using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin.Users;

public class UserManager : IUserManager
{


    public User Create(string username, string password)
    {
        var salt = Guid.NewGuid().ToString();
        var encryptedPassword = StringEncryptor.Encrypt($"{password}{salt}");

        return new User(
            id: Guid.NewGuid(),
            username: username,
            password: encryptedPassword,
            salt: salt);
    }
}
