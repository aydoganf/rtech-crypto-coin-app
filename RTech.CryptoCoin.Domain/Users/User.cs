namespace RTech.CryptoCoin.Users;

public class User : AuditedAggregateRoot<Guid>
{
    public virtual string Username { get; protected set; }
    public virtual string Password { get; protected set; }
    public virtual string Salt { get; protected set; }
    public virtual ICollection<Login> Logins { get; protected set; }

    protected User()
    {
        Logins = new List<Login>();
    }

    public User(Guid id, string username, string password, string salt) : base(id)
    {
        Logins = new List<Login>();

        Username = username;
        Password = password;
        Salt = salt;
    }

    public virtual void AddLogin(Guid id, string ipAddress)
    {
        Logins.Add(new Login(id, Id, ipAddress));
    }
}
