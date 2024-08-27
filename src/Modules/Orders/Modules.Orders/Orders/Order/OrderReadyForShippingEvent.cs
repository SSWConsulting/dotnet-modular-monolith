using Common.SharedKernel.Domain.Interfaces;

namespace Modules.Orders.Orders.Order;

internal record OrderReadyForShippingEvent(OrderId OrderId) : IDomainEvent
{
    public static OrderReadyForShippingEvent Create(Order order) => new(order.Id);
}
