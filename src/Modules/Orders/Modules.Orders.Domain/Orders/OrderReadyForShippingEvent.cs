using SharedKernel.Domain.Base;

namespace Modules.Orders.Domain.Orders;

public record OrderReadyForShippingEvent(OrderId OrderId) : DomainEvent
{
    public static OrderReadyForShippingEvent Create(Order order) => new(order.Id);
}