namespace GestionStock.Service.Auth
{
    public interface IPermissionService
    {
        IReadOnlyList<string> GetPermissionsForRoles(IReadOnlyList<string> roles);
    }
}
