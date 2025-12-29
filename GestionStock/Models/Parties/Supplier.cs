using GestionStock.Models.Order;

namespace GestionStock.Models.Parties
{
    public class Supplier : AuditableEntity
    {
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }
}
