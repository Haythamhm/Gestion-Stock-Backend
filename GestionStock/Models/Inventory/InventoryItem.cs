using GestionStock.Models.Products;

namespace GestionStock.Models.Inventory
{
    public class InventoryItem : EntityBase
    {
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = default!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public decimal OnHandQty { get; set; } = 0m;
        public decimal ReservedQty { get; set; } = 0m;

        // Optional: keep Available computed in code rather than stored in DB
        public decimal AvailableQty => OnHandQty - ReservedQty;

        // Concurrency token (optimistic concurrency)
        public byte[] RowVersion { get; set; } = default!;
    }
}
