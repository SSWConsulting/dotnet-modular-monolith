﻿using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Application.Common.Interfaces;
using Modules.Warehouse.Domain.Categories;
using Modules.Warehouse.Domain.Products;

namespace Modules.Warehouse.Infrastructure.Persistence;

public class WarehouseDbContext : DbContext, IWarehouseDbContext
{
    // private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    // private readonly OutboxInterceptor _outboxInterceptor;
    //
    // public DbSet<Product> Products { get; set; } = default!;
    //
    // public DbSet<Customer> Customers { get; set; } = default!;
    //
    // public DbSet<Order> Orders { get; set; } = default!;
    //
    // public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;
    //

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public WarehouseDbContext(DbContextOptions options /*EntitySaveChangesInterceptor saveChangesInterceptor, OutboxInterceptor outboxInterceptor*/) : base(options)
    {
        // _saveChangesInterceptor = saveChangesInterceptor;
        // _outboxInterceptor = outboxInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.AddInterceptors(
        //     _saveChangesInterceptor,
        //     _outboxInterceptor);
    }

    // public Task<int> SaveChangesAsync() => this
    // {
    //     throw new NotImplementedException();
    // }
}
