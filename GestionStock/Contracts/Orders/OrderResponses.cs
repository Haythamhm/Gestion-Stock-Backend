namespace GestionStock.Contracts.Orders
{
    public record OrderLineResponse(Guid ProductId, decimal Qty, decimal UnitPrice);

    public record PurchaseOrderResponse(
        Guid Id,
        string Number,
        Guid SupplierId,
        DateTime OrderDate,
        string Status,
        decimal Total,
        IReadOnlyList<OrderLineResponse> Lines
    );

    public record SalesOrderResponse(
        Guid Id,
        string Number,
        Guid CustomerId,
        Guid WarehouseId,
        DateTime OrderDate,
        string Status,
        decimal Total,
        IReadOnlyList<OrderLineResponse> Lines
    );
}
