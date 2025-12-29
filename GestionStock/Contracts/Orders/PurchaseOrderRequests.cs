using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Orders
{
    public record CreatePurchaseOrderRequest
    {
        [Required]
        public Guid SupplierId { get; init; }

        // Optionnel : si ton PO est lié à un entrepôt par défaut
        public Guid? TargetWarehouseId { get; init; }

        [MaxLength(NotesMax)]
        public string? Notes { get; init; }

        [Required, MinLength(1)]
        public List<PurchaseOrderLineRequest> Lines { get; init; } = new();
    }

    public record PurchaseOrderLineRequest
    {
        [Required]
        public Guid ProductId { get; init; }

        [Range(typeof(decimal), QtyMin, QtyMax)]
        public decimal Qty { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal UnitPrice { get; init; }
    }
}
