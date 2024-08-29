﻿using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Categories.Domain;
using Modules.Catalog.Products;

namespace Modules.Catalog.Common.Persistence;

public class CatalogDbContext : DbContext
{
    internal DbSet<Product> Products => Set<Product>();

    internal DbSet<Category> Categories => Set<Category>();

    // Needs to be public for the Database project
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
