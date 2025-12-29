using GestionStock.Models.Products;

namespace GestionStock.Models.Stock
{
    public class StockMovementLine : EntityBase
    {
        public Guid StockMovementId { get; set; }
        public StockMovement StockMovement { get; set; } = default!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }

        public string? LotNumber { get; set; }
        public string? SerialNumber { get; set; }
    }
}
