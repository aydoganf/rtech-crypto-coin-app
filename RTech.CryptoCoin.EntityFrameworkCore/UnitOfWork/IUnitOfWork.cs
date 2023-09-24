namespace RTech.CryptoCoin.UnitOfWork;

public interface IUnitOfWork : IDisposable
{

    event EventHandler<UnitOfWorkEventArgs> Disposed;
    Task CompleteAsync();
}
