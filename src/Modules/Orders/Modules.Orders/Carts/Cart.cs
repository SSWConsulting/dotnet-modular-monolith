using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Entities;
using Modules.Orders.Orders;

namespace Modules.Orders.Carts;

internal record CartId(Guid Value);

internal class Cart : AggregateRoot<CartId>
{
    private List<CartItem> _items = new();

    public void AddItem(CartItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(CartItem item)
    {
        _items.Remove(item);
    }
}

internal record CartItemId(Guid Value);

internal class CartItem : Entity<CartItemId>
{
    private ProductId _productId;
    private int _quantity;
    private Money _price;

    public CartItem(ProductId productId, int quantity, Money price)
    {
        _productId = productId;
        _quantity = quantity;
        _price = price;
    }
}
