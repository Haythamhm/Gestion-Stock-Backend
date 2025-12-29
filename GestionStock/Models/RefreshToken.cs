using GestionStock.Models.Users;

namespace GestionStock.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public required string TokenHash { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedAtUtc { get; set; }
        public string? ReplacedByTokenHash { get; set; }

        public bool IsActive => RevokedAtUtc is null && DateTime.UtcNow < ExpiresAtUtc;
    }
}
