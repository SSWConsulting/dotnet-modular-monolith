﻿using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Application.Common.Interfaces;
using Modules.Warehouse.Domain.Products;

namespace Modules.Warehouse.Application.Products;

public class ProductRepository : IProductRepository
{
    private readonly IWarehouseDbContext _dbContext;

    public ProductRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SkuExistsAsync(Sku sku)
    {
        return await _dbContext.Products.AnyAsync(p => p.Sku == sku);
    }

    public bool SkuExists(Sku sku)
    {
        return _dbContext.Products.Any(p => p.Sku == sku);
    }
}
