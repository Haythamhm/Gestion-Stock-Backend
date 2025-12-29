using System.ComponentModel.DataAnnotations;

namespace GestionStock.Contracts.Orders
{
    public record ApprovePurchaseOrderRequest
    {
        [Required]
        public Guid PurchaseOrderId { get; init; }
    }

    public record ReceivePurchaseOrderRequest
    {
        [Required]
        public Guid PurchaseOrderId { get; init; }

        [Required]
        public Guid WarehouseId { get; init; } // où tu réceptionnes

    }
}
