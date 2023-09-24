namespace RTech.CryptoCoin.Users;

public class Login : CreationAuditedEntity<Guid>
{
    public virtual Guid UserId { get; protected set; }
    public virtual string IpAddress { get; protected set; }

    protected Login()
    {        
    }

    public Login(Guid id, Guid userId, string ipAddress) : base(id)
    {
        UserId = userId;
        IpAddress = ipAddress;
    }
}