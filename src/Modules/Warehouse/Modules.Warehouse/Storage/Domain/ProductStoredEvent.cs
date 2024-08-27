using Common.SharedKernel.Domain.Base;
using Modules.Warehouse.Products.Domain;

namespace Modules.Warehouse.Storage.Domain;

internal record ProductStoredEvent(ProductId ProductId) : IDomainEvent;
