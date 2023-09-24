using Microsoft.EntityFrameworkCore;
using RTech.CryptoCoin.Data;
using RTech.CryptoCoin.EntityFrameworkCore;

namespace RTech.CryptoCoin.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly RTechCryptoCoinDbContext _dbContext;
    private bool IsDisposed;

    public UnitOfWork(RTechCryptoCoinDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public event EventHandler<UnitOfWorkEventArgs> Disposed = default!;

    public async Task CompleteAsync()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries())
        {
            if (entry.State is EntityState.Added && entry.HasCreationTime())
            {
                (entry.Entity as IHasCreationTime).CreationTime = DateTime.Now;
            }

            if (entry.State is EntityState.Modified && entry.HasModificationTime())
            {
                (entry.Entity as IHasModificationTime).LastModificationTime = DateTime.Now;
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;

        GC.SuppressFinalize(this);
    }
}