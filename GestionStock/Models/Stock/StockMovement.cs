using GestionStock.Enums;
using GestionStock.Models.Products;

namespace GestionStock.Models.Stock
{
    public class StockMovement : AuditableEntity
    {
        public required string Ref { get; set; } // unique ref (ex: MV-2026-0001)
        public MovementType Type { get; set; }
        public MovementStatus Status { get; set; } = MovementStatus.Draft;

        public Guid WarehouseFromId { get; set; }
        public Warehouse WarehouseFrom { get; set; } = default!;

        public Guid? WarehouseToId { get; set; }
        public Warehouse? WarehouseTo { get; set; }

        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        public ICollection<StockMovementLine> Lines { get; set; } = new List<StockMovementLine>();
    }
}
