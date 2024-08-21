using Ardalis.Specification;

namespace Modules.Orders.Orders.Order;

internal class OrderSpec : Specification<Order>
{
    public OrderSpec()
    {
        Query.Include(i => i.LineItems);
    }
}
