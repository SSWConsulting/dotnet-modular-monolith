using Ardalis.Specification;

namespace Modules.Orders.Orders;

internal class OrderSpec : Specification<Order>
{
    public OrderSpec()
    {
        Query.Include(i => i.LineItems);
    }
}
