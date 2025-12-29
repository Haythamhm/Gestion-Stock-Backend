using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Products
{
    public record CreateProductRequest
    {
        [Required, MaxLength(SkuMax)]
        [RegularExpression(SkuRegex)]
        public string SKU { get; init; } = default!;

        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        [MaxLength(100)]
        public string? Barcode { get; init; }

        [Required]
        public Guid CategoryId { get; init; }

        [Required]
        public Guid UomId { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal PurchasePrice { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal SalePrice { get; init; }

        public bool TrackLot { get; init; } = false;
        public bool TrackSerial { get; init; } = false;

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; init; } = 0;

        public bool IsActive { get; init; } = true;
    }

    public record UpdateProductRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        [MaxLength(100)]
        public string? Barcode { get; init; }

        [Required]
        public Guid CategoryId { get; init; }

        [Required]
        public Guid UomId { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal PurchasePrice { get; init; }

        [Range(typeof(decimal), MoneyMin, MoneyMax)]
        public decimal SalePrice { get; init; }

        public bool TrackLot { get; init; }
        public bool TrackSerial { get; init; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; init; }

        public bool IsActive { get; init; }
    }

    public record ProductSearchQuery : Contracts.Common.PagedQuery
    {
        [MaxLength(100)]
        public string? Q { get; init; }

        public Guid? CategoryId { get; init; }

        public Guid? WarehouseId { get; init; }

        public bool? OnlyActive { get; init; }
    }
}
