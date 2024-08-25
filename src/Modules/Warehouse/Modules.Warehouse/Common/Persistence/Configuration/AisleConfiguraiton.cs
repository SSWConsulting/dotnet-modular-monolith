using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Warehouse.Common.Persistence.Extensions;
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
            .ValueGeneratedNever();

        builder.HasMany(t => t.Bays)
            .WithOne()
            .IsRequired();

        builder.Property(m => m.Name)
            .IsRequired();

        // builder.OwnsMany(m => m.Bays, b =>
        // {
        //     // b.HasKey(m => m.Id);
        //     // b.Property(m => m.Id)
        //     //     .HasConversion(x => x.Value,
        //     //         x => new BayId(x))
        //     //     .ValueGeneratedNever();
        //     b.OwnsMany(m => m.Shelves, s =>
        //     {
        //         // s.HasKey(m => m.Id);
        //         s
        //             .Property(m => m.ProductId)!
        //             .HasStronglyTypedId<ProductId, Guid>()
        //              // .HasConversion<StronglyTypedIdConverter<ProductId, Guid>>()
        //             .ValueGeneratedNever();
        //     });
        // });
    }
}
