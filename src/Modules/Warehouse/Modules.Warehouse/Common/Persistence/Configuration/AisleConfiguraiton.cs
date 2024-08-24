using Common.SharedKernel.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Common.Persistence.Configuration;

internal class AisleConfiguration : IEntityTypeConfiguration<Aisle>
{
    public void Configure(EntityTypeBuilder<Aisle> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .HasStronglyTypedId<AisleId, Guid>()
            // .HasConversion<StronglyTypedIdConverter<AisleId, Guid>>()
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .IsRequired();

        builder.OwnsMany(m => m.Bays, b =>
        {
            // b.HasKey(m => m.Id);
            // b.Property(m => m.Id)
            //     .HasConversion(x => x.Value,
            //         x => new BayId(x))
            //     .ValueGeneratedNever();
            b.OwnsMany(m => m.Shelves, s =>
            {
                // s.HasKey(m => m.Id);
                s
                    .Property(m => m.ProductId)!
                    .HasStronglyTypedId<ProductId, Guid>()
                     // .HasConversion<StronglyTypedIdConverter<ProductId, Guid>>()
                    .ValueGeneratedNever();
            });
        });
    }
}

internal class StronglyTypedIdConverter<TId, TValue> : ValueConverter<TId, TValue>
    where TId : IStronglyTypedId<TValue>
{
    public StronglyTypedIdConverter(ConverterMappingHints? mappingHints = null)
        : base(
            id => id.Value,
            value => (TId)Activator.CreateInstance(typeof(TId), value)!,
            mappingHints)
    {
    }
}

internal static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TId> HasStronglyTypedId<TId, TValue>(this PropertyBuilder<TId> propertyBuilder)
        where TId : IStronglyTypedId<TValue>
    {
        return propertyBuilder.HasConversion(new StronglyTypedIdConverter<TId, TValue>());
    }
}

// internal class BayConfiguration : IEntityTypeConfiguration<Bay>
// {
//     public void Configure(EntityTypeBuilder<Bay> builder)
//     {
//         builder.HasKey(m => m.Id);
//
//         builder.OwnsMany(m => m.Shelves);
//     }
// }

// internal class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
// {
//     public void Configure(EntityTypeBuilder<Shelf> builder)
//     {
//         builder.HasKey(m => m.Id);
//     }
// }
