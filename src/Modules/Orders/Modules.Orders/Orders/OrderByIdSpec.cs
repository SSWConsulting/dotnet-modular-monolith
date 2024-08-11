﻿using Ardalis.Specification;

namespace Modules.Orders.Orders;

internal class OrderByIdSpec : OrderSpec, ISingleResultSpecification<Order>
{
    public OrderByIdSpec(OrderId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
