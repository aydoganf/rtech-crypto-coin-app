using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace RTech.CryptoCoin.Data;

public abstract class EfCoreRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    private readonly TDbContext DbContext;
    private readonly Lazy<EntityOptions<TEntity>> _entityOptionsLazy;
    private readonly IServiceProvider _serviceProvider;

    protected virtual EntityOptions<TEntity> EntityOptions => _entityOptionsLazy.Value;

    public EfCoreRepository(TDbContext dbContext, IServiceProvider serviceProvider)
    {
        DbContext = dbContext;
        _serviceProvider = serviceProvider;

        _entityOptionsLazy = new Lazy<EntityOptions<TEntity>>(
            () => _serviceProvider
                      .GetRequiredService<IOptions<EntityOptions>>()
                      .Value
                      .GetOrNull<TEntity>() ?? EntityOptions<TEntity>.Empty
        );
    }

    protected async Task<DbSet<TEntity>> GetDbSetAsync()
    {
        return await Task.FromResult(DbContext.Set<TEntity>());
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return (await GetDbSetAsync()).AsQueryable();
    }

    public async Task<IQueryable<TEntity>> WithIncludesAsync()
    {
        if (EntityOptions.IncludeFunc == null)
        {
            return await GetQueryableAsync();
        }

        return EntityOptions.IncludeFunc(await GetQueryableAsync());
    }

    public async Task DeleteAsync(TEntity entity)
    {
        (await GetDbSetAsync()).Remove(entity);
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await FindAsync(id);
        (await GetDbSetAsync()).Remove(entity);
    }

    public async Task<TEntity> FindAsync(TKey id)
    {
        return (await WithIncludesAsync()).SingleOrDefault(e => e.Id.Equals(id));
    }

    public async Task<TEntity> FindAsync(Func<TEntity, bool> predicate)
    {
        return (await WithIncludesAsync()).SingleOrDefault(predicate);
    }

    public async Task<List<TEntity>> GetListAsync()
    {
        return (await WithIncludesAsync()).ToList();
    }

    public async Task<List<TEntity>> GetListAsync(Func<TEntity, bool> predicate)
    {
        return (await WithIncludesAsync()).Where(predicate).ToList();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        if (typeof(TEntity).GetInterface(typeof(IHasCreationTime).Name) != null)
        {
            (entity as IHasCreationTime).CreationTime = DateTime.Now;
        }

        var entry = (await GetDbSetAsync()).Add(entity);

        return entry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = (await GetDbSetAsync()).Update(entity);

        return entry.Entity;
    }
}
