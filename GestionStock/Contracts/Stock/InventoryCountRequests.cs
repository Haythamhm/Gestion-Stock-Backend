using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Stock
{
    public record CreateInventoryCountRequest
    {
        [Required]
        public Guid WarehouseId { get; init; }

        [MaxLength(NotesMax)]
        public string? Notes { get; init; }
    }

    // Pendant le comptage: on envoie les quantités comptées
    public record SubmitInventoryCountLinesRequest
    {
        [Required]
        public Guid InventoryCountId { get; init; }

        [Required, MinLength(1)]
        public List<InventoryCountLineRequest> Lines { get; init; } = new();
    }

    public record InventoryCountLineRequest
    {
        [Required]
        public Guid ProductId { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)] 
        public decimal CountedQty { get; init; }
    }

    public record CloseInventoryCountRequest
    {
        [Required]
        public Guid InventoryCountId { get; init; }
    }
}
