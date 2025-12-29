namespace GestionStock.Contracts.Auth
{
    /// <summary>
    /// Réponse standard après Login / Refresh.
    /// </summary>
    public record AuthTokenResponse(
        string TokenType,            // "Bearer"
        string AccessToken,
        DateTime AccessTokenExpiresAtUtc,
        string RefreshToken,
        DateTime RefreshTokenExpiresAtUtc,
        UserIdentityDto User
    );

    public record LoginResponse(AuthTokenResponse Token);

    public record RegisterUserResponse(Guid UserId);

    public record MeResponse(UserIdentityDto User);
}
