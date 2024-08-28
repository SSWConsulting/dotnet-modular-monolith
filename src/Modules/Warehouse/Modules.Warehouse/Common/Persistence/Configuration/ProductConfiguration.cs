using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Warehouse.Products.Domain;

namespace Modules.Warehouse.Common.Persistence.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
              .HasConversion(productId => productId.Value, value => new ProductId(value));

        builder.Property(p => p.Sku)
            .HasConversion(sku => sku.Value, value => Sku.Create(value)!)
            .HasMaxLength(50);
    }
}
