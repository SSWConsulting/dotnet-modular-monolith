﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Application.Common.Interfaces;

namespace Modules.Warehouse.Application.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;

public record ProductDto(Guid Id, string Sku, string Name, decimal Price);

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IWarehouseDbContext _dbContext;

    public GetProductsQueryHandler(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Select(p => new ProductDto(p.Id.Value, p.Sku.Value, p.Name, p.Price.Amount))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
