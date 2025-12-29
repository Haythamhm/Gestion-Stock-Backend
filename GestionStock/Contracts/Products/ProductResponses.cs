namespace GestionStock.Contracts.Products
{
    public record ProductResponse(
    Guid Id,
    string SKU,
    string Name,
    string? Barcode,
    Guid CategoryId,
    Guid UomId,
    decimal PurchasePrice,
    decimal SalePrice,
    bool TrackLot,
    bool TrackSerial,
    int ReorderLevel,
    bool IsActive
);
}
