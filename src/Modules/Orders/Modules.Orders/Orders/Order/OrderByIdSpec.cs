﻿using Ardalis.Specification;

namespace Modules.Orders.Orders.Order;

internal class OrderByIdSpec : OrderSpec, ISingleResultSpecification<Order>
{
    public OrderByIdSpec(OrderId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
