using SharedKernel.Domain.Base;

namespace Modules.Warehouse.Domain.Categories;

public record CategoryCreatedEvent(CategoryId Id, string Name) : DomainEvent;