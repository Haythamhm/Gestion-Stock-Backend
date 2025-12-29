using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Orders
{
    public record CreateSalesOrderRequest
    {
        [Required]
        public Guid CustomerId { get; init; }

        // d'où on sort la marchandise
        [Required]
        public Guid WarehouseId { get; init; }

        [MaxLength(NotesMax)]
        public string? Notes { get; init; }

        [Required, MinLength(1)]
        public List<SalesOrderLineRequest> Lines { get; init; } = new();
    }

    public record SalesOrderLineRequest
    {
        [Required]
        public Guid ProductId { get; init; }

        [Range(typeof(decimal), QtyMin, QtyMax)]
        public decimal Qty { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal UnitPrice { get; init; }
    }
}
