using Common.SharedKernel.Domain.Interfaces;

namespace Common.SharedKernel.Domain.Base;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IReadOnlyList<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList().AsReadOnly();
        _domainEvents.Clear();

        return copy;
    }
}
