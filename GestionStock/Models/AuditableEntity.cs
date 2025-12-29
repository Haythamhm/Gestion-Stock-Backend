namespace GestionStock.Models
{
    public abstract class AuditableEntity : EntityBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedByUserId { get; set; }
    }
}
