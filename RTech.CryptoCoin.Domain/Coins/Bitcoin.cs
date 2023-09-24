namespace RTech.CryptoCoin.Coins;

public class Bitcoin : CreationAuditedEntity<Guid>
{
    public virtual decimal Value { get; protected set; }

    public Bitcoin(Guid id, decimal value) : base(id)
    {
        Value = value;
    }
}