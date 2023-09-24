namespace RTech.CryptoCoin.Data;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity> FindAsync(TKey id);
    Task<TEntity> FindAsync(Func<TEntity, bool> predicate);
    Task<List<TEntity>> GetListAsync();
    Task<List<TEntity>> GetListAsync(Func<TEntity, bool> predicate);
    
    
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(TKey id);

}
