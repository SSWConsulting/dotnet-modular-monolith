using Common.SharedKernel.Domain.Interfaces;

namespace Modules.Warehouse.Products.Domain;

internal record LowStockEvent(ProductId ProductId) : IDomainEvent;
