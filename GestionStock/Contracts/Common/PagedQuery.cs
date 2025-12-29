using System.ComponentModel.DataAnnotations;

namespace GestionStock.Contracts.Common
{
    public record PagedQuery
    {
        [Range(1, 10_000)]
        public int Page { get; init; } = 1;

        [Range(1, 200)]
        public int PageSize { get; init; } = 20;

        [MaxLength(50)]
        public string? SortBy { get; init; }

        public bool Desc { get; init; } = false;
    }
}
