using Common.SharedKernel.Domain.Base;

namespace Modules.Orders.Orders.Order;

internal record OrderReadyForShippingEvent(OrderId OrderId) : DomainEvent
{
    public static OrderReadyForShippingEvent Create(Order order) => new(order.Id);
}
