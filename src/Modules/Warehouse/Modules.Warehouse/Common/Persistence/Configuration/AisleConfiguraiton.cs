using Common.SharedKernel.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    }
}
