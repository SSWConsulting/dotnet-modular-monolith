using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Entities;
using Modules.Orders.Orders;

namespace Modules.Orders.Carts;

internal record CartId(Guid Value);

internal class Cart : AggregateRoot<CartId>
{
    private List<CartItem> _items = [];

    public Money TotalPrice { get; private set; } = null!;

    public static Cart Create(ProductId productId, int quantity, Money unitPrice)
    {
        var cart = new Cart
        {
            Id = new CartId(Guid.NewGuid())
        };

        cart.AddItem(productId, quantity, unitPrice);

        return cart;
    }

    public void AddItem(ProductId productId, int quantity, Money unitPrice)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            item.IncreaseQuantity(quantity);
        }
        else
        {
            var cartItem = CartItem.Create(productId, quantity, unitPrice);
            _items.Add(cartItem);
        }

        UpdateTotal();
    }

    public void RemoveItem(ProductId productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
            return;

        _items.Remove(item);
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        var currency = _items[0].UnitPrice.Currency;
        var total = _items.Sum(i => i.LinePrice.Amount);
        TotalPrice = new Money(currency, total);
    }
}
