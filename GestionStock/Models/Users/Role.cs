namespace GestionStock.Models.Users
{
    public class Role : EntityBase
    {
        public required string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
