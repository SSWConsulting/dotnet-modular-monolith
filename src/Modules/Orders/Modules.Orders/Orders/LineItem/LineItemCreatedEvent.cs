using Common.SharedKernel.Domain.Base;
using Modules.Orders.Orders.Order;

namespace Modules.Orders.Orders.LineItem;

internal record LineItemCreatedEvent(LineItemId LineItemId, OrderId Order) : DomainEvent
{
    public LineItemCreatedEvent(LineItem lineItem) : this(lineItem.Id, lineItem.OrderId) { }

    public static LineItemCreatedEvent Create(LineItem lineItem) => new(lineItem.Id, lineItem.OrderId);
}
