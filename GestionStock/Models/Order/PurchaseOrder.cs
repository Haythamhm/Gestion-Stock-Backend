using GestionStock.Enums;
using GestionStock.Models.Parties;

namespace GestionStock.Models.Order
{
    public class PurchaseOrder : AuditableEntity
    {
        public required string Number { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = default!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Draft;

        public decimal Total { get; set; }

        public ICollection<PurchaseOrderLine> Lines { get; set; } = new List<PurchaseOrderLine>();
    }
}
