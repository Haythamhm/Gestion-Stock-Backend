using GestionStock.Models;
using GestionStock.Models.Inventory;
using GestionStock.Models.Order;
using GestionStock.Models.Parties;
using GestionStock.Models.Products;
using GestionStock.Models.Sales;
using GestionStock.Models.Stock;
using GestionStock.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace GestionStock.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<UnitOfMeasure> Units => Set<UnitOfMeasure>();
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

        public DbSet<StockMovement> StockMovements => Set<StockMovement>();
        public DbSet<StockMovementLine> StockMovementLines => Set<StockMovementLine>();

        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();

        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SalesOrderLine> SalesOrderLines => Set<SalesOrderLine>();

        public DbSet<InventoryCount> InventoryCounts => Set<InventoryCount>();
        public DbSet<InventoryCountLine> InventoryCountLines => Set<InventoryCountLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserRole (many-to-many)
            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(x => x.RoleId);

            // Category self-reference
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraints
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.Code)
                .IsUnique();

            modelBuilder.Entity<StockMovement>()
                .HasIndex(m => m.Ref)
                .IsUnique();

            modelBuilder.Entity<PurchaseOrder>()
                .HasIndex(po => po.Number)
                .IsUnique();

            modelBuilder.Entity<SalesOrder>()
                .HasIndex(so => so.Number)
                .IsUnique();

            modelBuilder.Entity<InventoryCount>()
                .HasIndex(ic => ic.Number)
                .IsUnique();

            // InventoryItem unique: WarehouseId + ProductId
            modelBuilder.Entity<InventoryItem>()
                .HasIndex(i => new { i.WarehouseId, i.ProductId })
                .IsUnique();

            // Concurrency token
            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.RowVersion)
                .IsRowVersion();

            // StockMovement relationships (from/to warehouse)
            modelBuilder.Entity<StockMovement>()
                .HasOne(m => m.WarehouseFrom)
                .WithMany()
                .HasForeignKey(m => m.WarehouseFromId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(m => m.WarehouseTo)
                .WithMany()
                .HasForeignKey(m => m.WarehouseToId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovementLine>()
                .HasOne(l => l.StockMovement)
                .WithMany(m => m.Lines)
                .HasForeignKey(l => l.StockMovementId);

            // Purchase/Sales lines
            modelBuilder.Entity<PurchaseOrderLine>()
                .HasOne(l => l.PurchaseOrder)
                .WithMany(po => po.Lines)
                .HasForeignKey(l => l.PurchaseOrderId);

            modelBuilder.Entity<SalesOrderLine>()
                .HasOne(l => l.SalesOrder)
                .WithMany(so => so.Lines)
                .HasForeignKey(l => l.SalesOrderId);

            // Inventory count lines
            modelBuilder.Entity<InventoryCountLine>()
                .HasOne(l => l.InventoryCount)
                .WithMany(ic => ic.Lines)
                .HasForeignKey(l => l.InventoryCountId);

            // Numeric precision configuration
            modelBuilder.Entity<InventoryItem>()
                .Property(x => x.OnHandQty).HasPrecision(18, 4);

            modelBuilder.Entity<InventoryItem>()
                .Property(x => x.ReservedQty).HasPrecision(18, 4);

            modelBuilder.Entity<StockMovementLine>()
                .Property(x => x.Qty).HasPrecision(18, 4);

            modelBuilder.Entity<PurchaseOrderLine>()
                .Property(x => x.Qty).HasPrecision(18, 4);

            modelBuilder.Entity<SalesOrderLine>()
                .Property(x => x.Qty).HasPrecision(18, 4);

            modelBuilder.Entity<InventoryCountLine>()
                .Property(x => x.SystemQty).HasPrecision(18, 4);

            modelBuilder.Entity<InventoryCountLine>()
                .Property(x => x.CountedQty).HasPrecision(18, 4);

            modelBuilder.Entity<InventoryCountLine>()
                .Property(x => x.DeltaQty).HasPrecision(18, 4);

            // Money/prices: usually 18,2 (currency)
            modelBuilder.Entity<Product>()
                .Property(x => x.PurchasePrice).HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(x => x.SalePrice).HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseOrderLine>()
                .Property(x => x.UnitPrice).HasPrecision(18, 2);

            modelBuilder.Entity<SalesOrderLine>()
                .Property(x => x.UnitPrice).HasPrecision(18, 2);

            modelBuilder.Entity<StockMovementLine>()
                .Property(x => x.UnitCost).HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.Total).HasPrecision(18, 2);

            modelBuilder.Entity<SalesOrder>()
                .Property(x => x.Total).HasPrecision(18, 2);
        }
    }
}
