namespace GestionStock.Contracts.Auth
{
    public static class AuthRoles
    {
        public const string Admin = "Admin";
        public const string ResponsableStock = "ResponsableStock";
        public const string Magasinier = "Magasinier";
        public const string Acheteur = "Acheteur";
        public const string Vendeur = "Vendeur";
        public const string Comptable = "Comptable";
        public const string Auditeur = "Auditeur";
    }

    /// <summary>
    /// Optionnel mais utile pour un contrôle fin côté UI.
    /// Les permissions restent "front-friendly" (strings), faciles à gérer.
    /// </summary>
    public static class AuthPermissions
    {
        public const string ProductsRead = "products.read";
        public const string ProductsWrite = "products.write";

        public const string StockRead = "stock.read";
        public const string StockMove = "stock.move";

        public const string PurchaseRead = "purchase.read";
        public const string PurchaseWrite = "purchase.write";

        public const string SalesRead = "sales.read";
        public const string SalesWrite = "sales.write";

        public const string UsersRead = "users.read";
        public const string UsersWrite = "users.write";
    }

    public record UserIdentityDto(
        Guid Id,
        string Email,
        string FullName,
        bool IsActive,
        IReadOnlyList<string> Roles,
        IReadOnlyList<string> Permissions
    );
}
