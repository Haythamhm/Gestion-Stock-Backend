using GestionStock.Enums;
using GestionStock.Models.Products;

namespace GestionStock.Models.Inventory
{
    public class InventoryCount : AuditableEntity
    {
        public required string Number { get; set; } // unique
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = default!;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? CloseDate { get; set; }

        public CountStatus Status { get; set; } = CountStatus.Draft;

        public ICollection<InventoryCountLine> Lines { get; set; } = new List<InventoryCountLine>();
    }
}
