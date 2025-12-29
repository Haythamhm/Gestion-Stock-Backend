using GestionStock.Models.Inventory;

namespace GestionStock.Models.Products
{
    public class Warehouse : AuditableEntity
    {
        public required string Code { get; set; }   // unique
        public required string Name { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
