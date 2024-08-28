using Common.SharedKernel.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Common.Persistence.Configuration;

internal class BayConfiguration : IEntityTypeConfiguration<Bay>
{
    public void Configure(EntityTypeBuilder<Bay> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .HasStronglyTypedId<BayId, Guid>()
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .IsRequired();

        builder.HasMany(m => m.Shelves)
            .WithOne()
            .IsRequired();
    }
}
