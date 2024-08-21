using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Entities;

namespace Modules.Orders.Orders.Payment;

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
