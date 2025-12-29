using GestionStock.Contracts.Common;
using System.ComponentModel.DataAnnotations;

namespace GestionStock.Contracts.InventoryItemDTO
{
    public record InventoryQuery : PagedQuery
    {
        public Guid? WarehouseId { get; init; }
        public Guid? ProductId { get; init; }

        [MaxLength(100)]
        public string? Q { get; init; } // search by SKU/Name
    }

    public record InventoryItemResponse(
        Guid WarehouseId,
        string WarehouseCode,
        Guid ProductId,
        string SKU,
        string ProductName,
        decimal OnHandQty,
        decimal ReservedQty,
        decimal AvailableQty
    );
}
