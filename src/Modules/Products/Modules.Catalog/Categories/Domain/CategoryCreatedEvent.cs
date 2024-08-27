using Common.SharedKernel.Domain.Base;

namespace Modules.Catalog.Categories.Domain;

internal record CategoryCreatedEvent(CategoryId Id, string Name) : IDomainEvent;
