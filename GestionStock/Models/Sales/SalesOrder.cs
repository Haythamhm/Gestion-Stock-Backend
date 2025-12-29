using GestionStock.Enums;
using GestionStock.Models.Parties;

namespace GestionStock.Models.Sales
{
    public class SalesOrder : AuditableEntity
    {
        public required string Number { get; set; } // unique
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Draft;

        public decimal Total { get; set; }

        public ICollection<SalesOrderLine> Lines { get; set; } = new List<SalesOrderLine>();
    }
}
