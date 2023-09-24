namespace RTech.CryptoCoin.Data;

public interface IEntity<TKey> : IEntity where TKey : struct
{
    TKey Id { get; }
}


public interface IEntity
{

}