using GestionStock.Models.Products;

namespace GestionStock.Models.Inventory
{
    public class InventoryCountLine : EntityBase
    {
        public Guid InventoryCountId { get; set; }
        public InventoryCount InventoryCount { get; set; } = default!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public decimal SystemQty { get; set; }
        public decimal CountedQty { get; set; }
        public decimal DeltaQty { get; set; } // Counted - System
    }
}
