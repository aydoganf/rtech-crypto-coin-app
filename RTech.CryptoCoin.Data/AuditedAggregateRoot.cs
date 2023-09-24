using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin;

public abstract class AuditedAggregateRoot<TKey> : CreationAuditedEntity<TKey>, IHasModificationTime, IEntity<TKey> 
    where TKey : struct
{
    public DateTime? LastModificationTime { get; set; }

    public AuditedAggregateRoot()
    {
    }

    public AuditedAggregateRoot(TKey id)
    {
        Id = id;
    }
}