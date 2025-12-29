using GestionStock.Models.Products;

namespace GestionStock.Models.Sales
{
    public class SalesOrderLine : EntityBase
    {
        public Guid SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; } = default!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
