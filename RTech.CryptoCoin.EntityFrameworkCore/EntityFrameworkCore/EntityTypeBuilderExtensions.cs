using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin.EntityFrameworkCore;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureByConvention(this EntityTypeBuilder b)
    {
        b.TryConfigureCreationTime();
    }

    public static void TryConfigureCreationTime(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo(typeof(IHasCreationTime)))
        {
            b.Property(nameof(IHasCreationTime.CreationTime))
                .IsRequired()
                .HasColumnName(nameof(IHasCreationTime.CreationTime));
        }

        if (b.Metadata.ClrType.IsAssignableFrom(typeof(IHasModificationTime)))
        {
            b.Property(nameof(IHasModificationTime.LastModificationTime))
                .IsRequired()
                .HasColumnName(nameof(IHasModificationTime.LastModificationTime));
        }
    }
}
