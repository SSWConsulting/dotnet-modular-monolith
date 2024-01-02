﻿using Ardalis.Specification;

using SharedKernel.Domain.Identifiers;

namespace Modules.Warehouse.Domain.Products;

public class ProductByIdSpec : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductByIdSpec(ProductId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}