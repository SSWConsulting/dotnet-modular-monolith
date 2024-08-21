using Common.SharedKernel.Domain.Base;

namespace Modules.Orders.Orders.Order;

internal record OrderCreatedEvent(OrderId OrderId, CustomerId CustomerId) : DomainEvent
{
    public static OrderCreatedEvent Create(Order order) => new(order.Id, order.CustomerId);
}
