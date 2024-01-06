using Common.SharedKernel.Domain.Base;
using Modules.Orders.Domain.Customers;

namespace Modules.Orders.Domain.Orders;

public record OrderCreatedEvent(OrderId OrderId, CustomerId CustomerId) : DomainEvent
{
    public static OrderCreatedEvent Create(Order order) => new(order.Id, order.CustomerId);
}