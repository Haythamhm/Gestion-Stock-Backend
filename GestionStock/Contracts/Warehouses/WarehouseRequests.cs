using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Warehouses
{
    public record CreateWarehouseRequest
    {
        [Required, MaxLength(CodeMax)]
        [RegularExpression(CodeRegex)]
        public string Code { get; init; } = default!;

        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        [MaxLength(300)]
        public string? Address { get; init; }

        public bool IsActive { get; init; } = true;
    }

    public record UpdateWarehouseRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        [MaxLength(300)]
        public string? Address { get; init; }

        public bool IsActive { get; init; } = true;
    }
}
