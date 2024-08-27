using Common.SharedKernel.Domain.Base;

namespace Modules.Warehouse.Products.Domain;

internal record LowStockEvent(ProductId ProductId) : IDomainEvent;
