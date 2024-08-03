using Ardalis.Specification;

namespace Module.Orders.Orders;

internal class OrderByIdSpec : OrderSpec, ISingleResultSpecification<Order>
{
    public OrderByIdSpec(OrderId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
