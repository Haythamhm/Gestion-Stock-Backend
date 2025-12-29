namespace GestionStock.Models.Products
{
    public class UnitOfMeasure : EntityBase
    {
        public required string Code { get; set; } // ex: PCS, KG
        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
