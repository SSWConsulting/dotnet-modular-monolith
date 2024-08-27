using Common.SharedKernel.Domain.Base;

namespace Common.SharedKernel.Domain.Interfaces;

public interface IAggregateRoot
{
    // IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);

    // void RemoveDomainEvent(IDomainEvent domainEvent);

    // void ClearDomainEvents();

    IReadOnlyList<IDomainEvent> PopDomainEvents();
}
