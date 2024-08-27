using Common.SharedKernel.Domain.Interfaces;

namespace Modules.Catalog.Categories.Domain;

internal record CategoryCreatedEvent(CategoryId Id, string Name) : IDomainEvent;
