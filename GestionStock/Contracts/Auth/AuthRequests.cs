using System.ComponentModel.DataAnnotations;

namespace GestionStock.Contracts.Auth
{
    public record LoginRequest
    {
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; init; } = default!;

        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; init; } = default!;
    }

    /// <summary>
    /// En général: création d’un user par Admin.
    /// </summary>
    public record RegisterUserRequest
    {
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; init; } = default!;

        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; init; } = default!;

        [Required, MaxLength(200)]
        public string FullName { get; init; } = default!;

        /// <summary>
        /// Exemple: ["Admin"] ou ["Magasinier","Vendeur"].
        /// On valide plus finement côté service (roles exist, etc.)
        /// </summary>
        [Required, MinLength(1)]
        public List<string> Roles { get; init; } = new();

        public bool IsActive { get; init; } = true;
    }

    public record RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; init; } = default!;
    }

    public record ChangePasswordRequest
    {
        [Required, MinLength(8), MaxLength(100)]
        public string CurrentPassword { get; init; } = default!;

        [Required, MinLength(8), MaxLength(100)]
        public string NewPassword { get; init; } = default!;
    }
}
