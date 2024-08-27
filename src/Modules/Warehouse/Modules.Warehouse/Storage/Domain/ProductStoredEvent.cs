using Common.SharedKernel.Domain.Interfaces;
using Modules.Warehouse.Products.Domain;

namespace Modules.Warehouse.Storage.Domain;

internal record ProductStoredEvent(ProductId ProductId) : IDomainEvent;
