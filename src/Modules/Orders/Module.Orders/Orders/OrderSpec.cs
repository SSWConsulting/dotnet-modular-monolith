using Ardalis.Specification;

namespace Module.Orders.Orders;

internal class OrderSpec : Specification<Order>
{
    public OrderSpec()
    {
        Query.Include(i => i.LineItems);
    }
}
