using System.ComponentModel.DataAnnotations;

namespace GestionStock.Contracts.Orders
{
    public record ConfirmSalesOrderRequest
    {
        [Required]
        public Guid SalesOrderId { get; init; }
    }

    public record ShipSalesOrderRequest
    {
        [Required]
        public Guid SalesOrderId { get; init; }

        public DateTime? ShipDateUtc { get; init; }
    }
}
