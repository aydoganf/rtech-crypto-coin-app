namespace RTech.CryptoCoin.Data;

public interface IHasModificationTime
{
    DateTime? LastModificationTime { get; set; }
}