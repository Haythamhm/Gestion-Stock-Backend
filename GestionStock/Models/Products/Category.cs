namespace GestionStock.Models.Products
{
    public class Category : EntityBase
    {
        public required string Name { get; set; }

        public Guid? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category> Children { get; set; } = new List<Category>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
