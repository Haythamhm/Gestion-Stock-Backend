using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.UoM
{
    public record CreateUnitRequest
    {
        [Required, MaxLength(CodeMax)]
        [RegularExpression(CodeRegex)]
        public string Code { get; init; } = default!; // PCS, KG...

        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;
    }

    public record UpdateUnitRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;
    }

    public record UnitResponse(Guid Id, string Code, string Name);
}
