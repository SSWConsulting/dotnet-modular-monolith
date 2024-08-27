using Common.SharedKernel.Domain.Interfaces;

namespace Modules.Customers.Customers;

internal record CustomerCreatedEvent(CustomerId Id, string FirstName, string LastName) : IDomainEvent
{
    public static CustomerCreatedEvent Create(Customer customer) =>
        new CustomerCreatedEvent(customer.Id, customer.FirstName, customer.LastName);
}
