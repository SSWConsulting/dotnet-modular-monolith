﻿using Common.SharedKernel.Domain.Base;

namespace Modules.Orders.Domain.Orders;

public record LineItemCreatedEvent(LineItemId LineItemId, OrderId Order) : DomainEvent
{
    public LineItemCreatedEvent(LineItem lineItem) : this(lineItem.Id, lineItem.OrderId) { }

    public static LineItemCreatedEvent Create(LineItem lineItem) => new(lineItem.Id, lineItem.OrderId);
}
