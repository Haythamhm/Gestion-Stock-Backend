using System.ComponentModel.DataAnnotations;
using static GestionStock.Contracts.Common.ValidationConstants;

namespace GestionStock.Contracts.Partners
{
    public record CreateSupplierRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        [Phone, MaxLength(30)]
        public string? Phone { get; init; }

        [EmailAddress, MaxLength(150)]
        public string? Email { get; init; }

        [MaxLength(300)]
        public string? Address { get; init; }

        public bool IsActive { get; init; } = true;
    }

    public record UpdateSupplierRequest : CreateSupplierRequest;

    public record CreateCustomerRequest
    {
        [Required, MaxLength(NameMax)]
        public string Name { get; init; } = default!;

        [Phone, MaxLength(30)]
        public string? Phone { get; init; }

        [EmailAddress, MaxLength(150)]
        public string? Email { get; init; }

        [MaxLength(300)]
        public string? Address { get; init; }

        public bool IsActive { get; init; } = true;
    }

    public record UpdateCustomerRequest : CreateCustomerRequest;

    public record PartnerResponse(Guid Id, string Name, string? Phone, string? Email, string? Address, bool IsActive);
}
