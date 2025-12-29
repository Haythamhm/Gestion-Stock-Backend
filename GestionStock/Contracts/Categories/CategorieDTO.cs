using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Categories
{
    public record CreateCategoryRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        public Guid? ParentId { get; init; }
    }

    public record UpdateCategoryRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        public Guid? ParentId { get; init; }
    }

    public record CategoryResponse(Guid Id, string Name, Guid? ParentId);
}
