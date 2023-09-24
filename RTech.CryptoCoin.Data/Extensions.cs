using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RTech.CryptoCoin.Data;

public static class Extensions
{
    public static bool HasCreationTime(this EntityEntry entry) 
        => typeof(IHasCreationTime).IsAssignableFrom(entry.Entity.GetType());

    public static bool HasModificationTime(this EntityEntry entry)
        => typeof(IHasModificationTime).IsAssignableFrom(entry.Entity.GetType());
}
