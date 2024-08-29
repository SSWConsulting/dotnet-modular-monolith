using Common.SharedKernel.Persistence;
using Common.SharedKernel.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Products.Domain;

namespace Modules.Catalog.Common.Persistence.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .HasStronglyTypedId<ProductId, Guid>()
            .ValueGeneratedNever();

        builder.HasMany(t => t.Categories)
            .WithMany();

        builder.Property(m => m.Name)
            .IsRequired();

        builder.Property(m => m.Sku)
            .IsRequired();

        builder.ComplexProperty(m => m.Price, MoneyConfiguration.BuildAction);
    }
}
