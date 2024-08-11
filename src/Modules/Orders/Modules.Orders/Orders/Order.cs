using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Entities;
using Common.SharedKernel.Domain.Exceptions;
using ErrorOr;
using Success = ErrorOr.Success;

namespace Modules.Orders.Orders;

internal class Order : AggregateRoot<OrderId>
{
    private readonly List<LineItem> _lineItems = [];

    public IEnumerable<LineItem> LineItems => _lineItems.AsReadOnly();

    public required CustomerId CustomerId { get; init; }

    // TODO: Check FE overrides this
    public Money AmountPaid { get; private set; } = null!;

    private Payment _payment = null!;

    public OrderStatus Status { get; private set; }

    public DateTimeOffset ShippingDate { get; private set; }

    public Currency? OrderCurrency => _lineItems.FirstOrDefault()?.Price.Currency;

    public Money OrderTotal
    {
        get
        {
            if (_lineItems.Count == 0)
                return Money.Default;

            var amount = _lineItems.Sum(li => li.Price.Amount * li.Quantity);
            var currency = _lineItems[0].Price.Currency;

            return new Money(currency, amount);
        }
    }

    private Order()
    {
    }

    public static Order Create(CustomerId customerId)
    {
        var order = new Order()
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
            AmountPaid = Money.Default,
            Status = OrderStatus.PendingPayment
        };

        order.AddDomainEvent(OrderCreatedEvent.Create(order));

        return order;
    }

    public ErrorOr<LineItem> AddLineItem(ProductId productId, Money price, int quantity)
    {
        // TODO: Unit test
        if (Status == OrderStatus.PendingPayment)
            return OrderErrors.CantModifyAfterPayment;

        // TODO: Unit test
        if (OrderCurrency != null && OrderCurrency != price.Currency)
            return OrderErrors.CurrencyMismatch;

        var existingLineItem = _lineItems.FirstOrDefault(li => li.ProductId == productId);
        if (existingLineItem != null)
        {
            existingLineItem.AddQuantity(quantity);
            return existingLineItem;
        }

        var lineItem = LineItem.Create(Id, productId, price, quantity);
        AddDomainEvent(new LineItemCreatedEvent(lineItem.Id, lineItem.OrderId));
        _lineItems.Add(lineItem);

        return lineItem;
    }

    public ErrorOr<Success> RemoveLineItem(ProductId productId)
    {
        if (Status == OrderStatus.PendingPayment)
            return OrderErrors.CantModifyAfterPayment;

        var lineItem = _lineItems.RemoveAll(x => x.ProductId == productId);

        return Result.Success;
    }

    public ErrorOr<Success> AddPayment(Money payment)
    {
        if (payment.Amount <= 0)
            return OrderErrors.PaymentAmountZeroOrNegative;

        if (payment > OrderTotal - AmountPaid)
            return OrderErrors.PaymentExceedsOrderTotal;

        // Ensure currency is set on first payment
        if (AmountPaid.Amount == 0)
            AmountPaid = payment;
        else
            AmountPaid += payment;

        if (AmountPaid >= OrderTotal)
        {
            Status = OrderStatus.ReadyForShipping;
            AddDomainEvent(new OrderReadyForShippingEvent(Id));
        }

        return Result.Success;
    }

    public void AddQuantity(ProductId productId, int quantity) =>
        _lineItems.FirstOrDefault(li => li.ProductId == productId)?.AddQuantity(quantity);

    public void RemoveQuantity(ProductId productId, int quantity) =>
        _lineItems.FirstOrDefault(li => li.ProductId == productId)?.RemoveQuantity(quantity);

    public ErrorOr<Success> ShipOrder(TimeProvider timeProvider)
    {
        if (Status == OrderStatus.PendingPayment)
            return OrderErrors.CantShipUnpaidOrder;

        if (Status == OrderStatus.InTransit)
            return OrderErrors.OrderAlreadyShipped;

        if (_lineItems.Sum(li => li.Quantity) <= 0)
            throw new DomainException("Can't ship an order with no items");

        ShippingDate = timeProvider.GetUtcNow();
        Status = OrderStatus.InTransit;

        return Result.Success;
    }
}

internal record PaymentId(Guid Value);

internal class Payment : Entity<PaymentId>
{
    public Money Amount { get; private set; }

    public PaymentType PaymentType { get; private set; }

    public Payment(Money amount, PaymentType paymentType)
    {
        Amount = amount;
        PaymentType = paymentType;
    }
}

// TODO: Convert to Smart Enums
internal enum PaymentType
{
    CreditCard = 1,
    PayPal = 2,
    Cash = 3
}
