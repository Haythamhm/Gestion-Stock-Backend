using GestionStock.Contracts.Auth;

namespace GestionStock.Service.Auth
{
    public sealed class PermissionService : IPermissionService
    {
        private static readonly Dictionary<string, string[]> RolePermissions = new()
        {
            [AuthRoles.Admin] = new[]
            {
            AuthPermissions.ProductsRead, AuthPermissions.ProductsWrite,
            AuthPermissions.StockRead, AuthPermissions.StockMove,
            AuthPermissions.PurchaseRead, AuthPermissions.PurchaseWrite,
            AuthPermissions.SalesRead, AuthPermissions.SalesWrite,
            AuthPermissions.UsersRead, AuthPermissions.UsersWrite
        },
            [AuthRoles.Magasinier] = new[]
            {
            AuthPermissions.ProductsRead, AuthPermissions.StockRead, AuthPermissions.StockMove
        },
            [AuthRoles.ResponsableStock] = new[]
            {
            AuthPermissions.ProductsRead, AuthPermissions.ProductsWrite,
            AuthPermissions.StockRead, AuthPermissions.StockMove
        },
            [AuthRoles.Acheteur] = new[]
            {
            AuthPermissions.PurchaseRead, AuthPermissions.PurchaseWrite, AuthPermissions.ProductsRead
        },
            [AuthRoles.Vendeur] = new[]
            {
            AuthPermissions.SalesRead, AuthPermissions.SalesWrite, AuthPermissions.StockRead
        },
            [AuthRoles.Auditeur] = new[]
            {
            AuthPermissions.ProductsRead, AuthPermissions.StockRead, AuthPermissions.PurchaseRead, AuthPermissions.SalesRead
        }
        };

        public IReadOnlyList<string> GetPermissionsForRoles(IReadOnlyList<string> roles)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in roles)
            {
                if (RolePermissions.TryGetValue(r, out var perms))
                    foreach (var p in perms) set.Add(p);
            }
            return set.ToArray();
        }
    }
}
