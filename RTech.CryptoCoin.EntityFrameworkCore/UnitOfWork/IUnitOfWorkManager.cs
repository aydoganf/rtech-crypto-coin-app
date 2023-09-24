namespace RTech.CryptoCoin.UnitOfWork;

public interface IUnitOfWorkManager
{
    IUnitOfWork Create();
}
