using Common.SharedKernel.Domain.Base;

namespace Common.SharedKernel.Domain.Interfaces;

public interface IDomainEvents
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }

    void AddDomainEvent(DomainEvent domainEvent);

    void RemoveDomainEvent(DomainEvent domainEvent);

    void ClearDomainEvents();
}
