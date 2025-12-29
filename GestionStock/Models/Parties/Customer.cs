using GestionStock.Models.Sales;

namespace GestionStock.Models.Parties
{
    public class Customer : AuditableEntity
    {
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
    }
}
