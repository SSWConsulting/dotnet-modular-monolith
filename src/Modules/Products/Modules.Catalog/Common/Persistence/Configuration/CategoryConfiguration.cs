using Common.SharedKernel.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Categories.Domain;

namespace Modules.Catalog.Common.Persistence.Configuration;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasStronglyTypedId<CategoryId, Guid>()
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasMaxLength(50);
    }
}
