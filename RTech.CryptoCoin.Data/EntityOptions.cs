namespace RTech.CryptoCoin.Data;

public class EntityOptions<TEntity>
    where TEntity : IEntity
{
    public static EntityOptions<TEntity> Empty { get; } = new EntityOptions<TEntity>();

    public Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeFunc { get; set; }
}

public class EntityOptions
{
    private readonly IDictionary<Type, object> _options;

    public EntityOptions()
    {
        _options = new Dictionary<Type, object>();
    }

    public EntityOptions<TEntity> GetOrNull<TEntity>()
        where TEntity : IEntity
    {
        return _options.TryGetValue(typeof(TEntity), out object option) ? (EntityOptions<TEntity>)option : EntityOptions<TEntity>.Empty;
    }

    public void Entity<TEntity>(Action<EntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity
    {

        if (_options.ContainsKey(typeof(TEntity)) == false)
        {
            _options.Add(typeof(TEntity), new EntityOptions<TEntity>());
        }

        optionsAction(_options[typeof(TEntity)] as EntityOptions<TEntity>);
    }
}
