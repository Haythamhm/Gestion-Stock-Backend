using GestionStock.Contracts.Products;

namespace GestionStock.Service.Catalog
{
    public interface IProductService
    {
        Task<Guid> CreateAsync(CreateProductRequest req, Guid userId, CancellationToken ct);
        Task UpdateAsync(Guid id, UpdateProductRequest req, Guid userId, CancellationToken ct);
        Task<ProductResponse> GetByIdAsync(Guid id, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct); // soft delete recommandé
    }
}
