using GestionStock.Enums;

namespace GestionStock.Models.Users
{
    public class User : AuditableEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
