using Common.SharedKernel.Domain.Base;
using Throw;

namespace Modules.Customers.Customers;

internal class Customer : AggregateRoot<CustomerId>
{
    public string Email { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public Address? Address { get; private set; }

    private Customer() { }

    internal static Customer Create(string email, string firstName, string lastName)
    {
        email.Throw().IfEmpty();

        var customer = new Customer() { Id = new CustomerId(Guid.NewGuid()), Email = email, };

        customer.UpdateName(firstName, lastName);
        customer.AddDomainEvent(CustomerCreatedEvent.Create(customer));

        return customer;
    }

    public void UpdateName(string firstName, string lastName)
    {
        firstName.Throw().IfEmpty();
        lastName.Throw().IfEmpty();

        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateAddress(Address address)
    {
        Address = address;
    }
}
