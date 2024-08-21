using Ardalis.SmartEnum;

namespace Modules.Orders.Orders.Order;

// internal enum OrderStatus
// {
//     None = 0,
//     PendingPayment = 1,
//     ReadyForShipping = 2,
//     InTransit = 3
// }

internal class OrderStatus : SmartEnum<OrderStatus>
{
    public static readonly OrderStatus None = new(0, "None");
    public static readonly OrderStatus PendingPayment = new(1, "PendingPayment");
    public static readonly OrderStatus ReadyForShipping = new(2, "ReadyForShipping");
    public static readonly OrderStatus InTransit = new(3, "InTransit");
    public static readonly OrderStatus Delivered = new(4, "Delivered");

    private OrderStatus(int id, string name) : base(name, id)
    {
    }
}
