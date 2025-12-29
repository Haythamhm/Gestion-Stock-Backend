using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Stock
{
    public enum MovementTypeDto { In = 1, Out = 2, Transfer = 3, Adjustment = 4 }

    public record CreateStockMovementRequest
    {
        [Required]
        [EnumDataType(typeof(MovementTypeDto))]
        public MovementTypeDto Type { get; init; }

        [Required]
        public Guid WarehouseFromId { get; init; }

        // obligatoire seulement si Transfer (à valider côté service aussi)
        public Guid? WarehouseToId { get; init; }

        [MaxLength(NotesMax)]
        public string? Notes { get; init; }

        [Required, MinLength(1)]
        public List<StockMovementLineRequest> Lines { get; init; } = new();
    }

    public record StockMovementLineRequest
    {
        [Required]
        public Guid ProductId { get; init; }

        [Range(typeof(decimal), QtyMin, QtyMax)]
        public decimal Qty { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal UnitCost { get; init; }

        [MaxLength(80)]
        public string? LotNumber { get; init; }

        [MaxLength(80)]
        public string? SerialNumber { get; init; }
    }
}
