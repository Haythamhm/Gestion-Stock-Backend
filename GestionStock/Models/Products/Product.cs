using GestionStock.Models.Inventory;

namespace GestionStock.Models.Products
{
    public class Product : AuditableEntity
    {
        public required string SKU { get; set; }
        public required string Name { get; set; }
        public string? Barcode { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public Guid UomId { get; set; }
        public UnitOfMeasure Uom { get; set; } = default!;

        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public bool TrackLot { get; set; } = false;
        public bool TrackSerial { get; set; } = false;

        public int ReorderLevel { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
