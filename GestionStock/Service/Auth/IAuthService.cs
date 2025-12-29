using GestionStock.Contracts.Auth;

namespace GestionStock.Service.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct);
        Task<AuthTokenResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken ct);

        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, Guid createdByUserId, CancellationToken ct);
        Task ChangePasswordAsync(Guid userId, ChangePasswordRequest request, CancellationToken ct);

        Task<MeResponse> GetMeAsync(Guid userId, CancellationToken ct);
    }
}
