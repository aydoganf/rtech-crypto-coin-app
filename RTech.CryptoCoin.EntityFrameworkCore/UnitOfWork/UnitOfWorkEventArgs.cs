namespace RTech.CryptoCoin.UnitOfWork;

public class UnitOfWorkEventArgs : EventArgs
{
    public IUnitOfWork UnitOfWork { get; }

    public UnitOfWorkEventArgs(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}


