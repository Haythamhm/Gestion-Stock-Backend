using GestionStock.Contracts.Products;
using GestionStock.Data;
using GestionStock.Models.Products;
using GestionStock.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace GestionStock.Service.Catalog
{
    public sealed class ProductService : IProductService
    {
        private readonly StockDbContext _db;
        public ProductService(StockDbContext db) => _db = db;

        public async Task<Guid> CreateAsync(CreateProductRequest req, Guid userId, CancellationToken ct)
        {
            if (await _db.Products.AnyAsync(p => p.SKU == req.SKU, ct))
                throw new BusinessRuleException("SKU déjà existant.");

            // existence refs
            _ = await _db.Categories.FirstOrDefaultAsync(c => c.Id == req.CategoryId, ct)
                ?? throw new BusinessRuleException("Catégorie invalide.");
            _ = await _db.Units.FirstOrDefaultAsync(u => u.Id == req.UomId, ct)
                ?? throw new BusinessRuleException("Unité invalide.");

            var p = new Product
            {
                SKU = req.SKU,
                Name = req.Name,
                Barcode = req.Barcode,
                CategoryId = req.CategoryId,
                UomId = req.UomId,
                PurchasePrice = req.PurchasePrice,
                SalePrice = req.SalePrice,
                TrackLot = req.TrackLot,
                TrackSerial = req.TrackSerial,
                ReorderLevel = req.ReorderLevel,
                IsActive = req.IsActive,
                CreatedByUserId = userId
            };

            _db.Products.Add(p);
            await _db.SaveChangesAsync(ct);
            return p.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateProductRequest req, Guid userId, CancellationToken ct)
        {
            var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id, ct)
                ?? throw new NotFoundException("Produit introuvable.");

            _ = await _db.Categories.FirstOrDefaultAsync(c => c.Id == req.CategoryId, ct)
                ?? throw new BusinessRuleException("Catégorie invalide.");
            _ = await _db.Units.FirstOrDefaultAsync(u => u.Id == req.UomId, ct)
                ?? throw new BusinessRuleException("Unité invalide.");

            p.Name = req.Name;
            p.Barcode = req.Barcode;
            p.CategoryId = req.CategoryId;
            p.UomId = req.UomId;
            p.PurchasePrice = req.PurchasePrice;
            p.SalePrice = req.SalePrice;
            p.TrackLot = req.TrackLot;
            p.TrackSerial = req.TrackSerial;
            p.ReorderLevel = req.ReorderLevel;
            p.IsActive = req.IsActive;
            p.UpdatedAt = DateTime.UtcNow;
            p.UpdatedByUserId = userId;

            await _db.SaveChangesAsync(ct);
        }

        public async Task<ProductResponse> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var p = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct)
                ?? throw new NotFoundException("Produit introuvable.");

            return new ProductResponse(
                p.Id, p.SKU, p.Name, p.Barcode, p.CategoryId, p.UomId,
                p.PurchasePrice, p.SalePrice, p.TrackLot, p.TrackSerial,
                p.ReorderLevel, p.IsActive);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id, ct)
                ?? throw new NotFoundException("Produit introuvable.");

            p.IsActive = false; // soft delete
            await _db.SaveChangesAsync(ct);
        }
    }
}
