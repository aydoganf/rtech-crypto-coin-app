using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin;

public abstract class CreationAuditedEntity<TKey> : IHasCreationTime, IEntity<TKey> 
    where TKey : struct
{
    public TKey Id { get; set; }
    public DateTime CreationTime { get; set; }

    public CreationAuditedEntity()
    {   
    }

    public CreationAuditedEntity(TKey id)
    {
        Id = id;
    }
}
