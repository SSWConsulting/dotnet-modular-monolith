using Common.SharedKernel.Domain.Base;

namespace Modules.Catelog.Categories.Domain;

internal record CategoryCreatedEvent(CategoryId Id, string Name) : DomainEvent;
