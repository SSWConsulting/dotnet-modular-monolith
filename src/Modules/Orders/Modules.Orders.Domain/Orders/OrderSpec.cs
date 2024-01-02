using Ardalis.Specification;

namespace Modules.Orders.Domain.Orders;

public class OrderSpec : Specification<Order>
{
    public OrderSpec()
    {
        Query.Include(i => i.LineItems);
    }
}