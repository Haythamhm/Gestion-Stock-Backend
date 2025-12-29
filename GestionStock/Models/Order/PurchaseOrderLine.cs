using GestionStock.Models.Products;

namespace GestionStock.Models.Order
{
    public class PurchaseOrderLine : EntityBase
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; } = default!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
