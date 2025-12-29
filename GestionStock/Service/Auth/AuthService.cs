using GestionStock.Contracts.Auth;
using GestionStock.Data;
using GestionStock.Models;
using GestionStock.Models.Users;
using GestionStock.Service.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GestionStock.Service.Auth
{
    public sealed record JwtOptions
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required string SigningKey { get; init; } // secret key
        public int AccessTokenMinutes { get; init; } = 30;
        public int RefreshTokenDays { get; init; } = 14;
    }

    public sealed class AuthService : IAuthService
    {
        private readonly StockDbContext _db;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IPermissionService _permissionService;
        private readonly JwtOptions _jwt;

        public AuthService(
            StockDbContext db,
            IPasswordHasher<User> hasher,
            IPermissionService permissionService,
            IOptions<JwtOptions> jwtOptions)
        {
            _db = db;
            _hasher = hasher;
            _permissionService = permissionService;
            _jwt = jwtOptions.Value;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            var user = await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email, ct);

            if (user is null || !user.IsActive)
                throw new BusinessRuleException("Email ou mot de passe invalide.");

            var verify = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verify == PasswordVerificationResult.Failed)
                throw new BusinessRuleException("Email ou mot de passe invalide.");

            var roles = user.UserRoles.Select(r => r.Role.Name).Distinct().ToArray();
            var permissions = _permissionService.GetPermissionsForRoles(roles);

            var token = await IssueTokensAsync(user, roles, permissions, ct);
            return new LoginResponse(token);
        }

        public async Task<AuthTokenResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken ct)
        {
            // On stocke un hash en DB (jamais le token brut)
            var tokenHash = HashToken(request.RefreshToken);

            var stored = await _db.RefreshTokens
                .Include(rt => rt.User).ThenInclude(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, ct);

            if (stored is null || !stored.IsActive)
                throw new BusinessRuleException("Refresh token invalide ou expiré.");

            var user = stored.User;
            if (!user.IsActive)
                throw new BusinessRuleException("Compte désactivé.");

            // rotation: on révoque l'ancien et on émet un nouveau
            stored.RevokedAtUtc = DateTime.UtcNow;

            var roles = user.UserRoles.Select(r => r.Role.Name).Distinct().ToArray();
            var permissions = _permissionService.GetPermissionsForRoles(roles);

            var token = await IssueTokensAsync(user, roles, permissions, ct, rotateFrom: stored);
            await _db.SaveChangesAsync(ct);

            return token;
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, Guid createdByUserId, CancellationToken ct)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == request.Email, ct);
            if (exists) throw new BusinessRuleException("Email déjà utilisé.");

            // roles doivent exister en DB (ou tu les seeds au démarrage)
            var roles = await _db.Roles.Where(r => request.Roles.Contains(r.Name)).ToListAsync(ct);
            if (roles.Count != request.Roles.Count)
                throw new BusinessRuleException("Un ou plusieurs rôles sont invalides.");

            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                IsActive = request.IsActive,
                PasswordHash = "TEMP",
                CreatedByUserId = createdByUserId
            };

            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            foreach (var role in roles)
                user.UserRoles.Add(new UserRole { User = user, Role = role });

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);
            return new RegisterUserResponse(user.Id);
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordRequest request, CancellationToken ct)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId, ct)
                ?? throw new NotFoundException("Utilisateur introuvable.");

            var verify = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.CurrentPassword);
            if (verify == PasswordVerificationResult.Failed)
                throw new BusinessRuleException("Mot de passe actuel incorrect.");

            user.PasswordHash = _hasher.HashPassword(user, request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedByUserId = userId;

            await _db.SaveChangesAsync(ct);
        }

        public async Task<MeResponse> GetMeAsync(Guid userId, CancellationToken ct)
        {
            var user = await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId, ct)
                ?? throw new NotFoundException("Utilisateur introuvable.");

            var roles = user.UserRoles.Select(r => r.Role.Name).Distinct().ToArray();
            var permissions = _permissionService.GetPermissionsForRoles(roles);

            return new MeResponse(new UserIdentityDto(
                user.Id, user.Email, user.FullName, user.IsActive, roles, permissions));
        }

        private async Task<AuthTokenResponse> IssueTokensAsync(
            User user,
            IReadOnlyList<string> roles,
            IReadOnlyList<string> permissions,
            CancellationToken ct,
            RefreshToken? rotateFrom = null)
        {
            var now = DateTime.UtcNow;
            var accessExp = now.AddMinutes(_jwt.AccessTokenMinutes);
            var refreshExp = now.AddDays(_jwt.RefreshTokenDays);

            var accessToken = CreateJwt(user, roles, permissions, accessExp);

            var refreshTokenRaw = CreateSecureRandomToken();
            var refreshHash = HashToken(refreshTokenRaw);

            if (rotateFrom is not null)
                rotateFrom.ReplacedByTokenHash = refreshHash;

            _db.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAtUtc = refreshExp
            });

            await _db.SaveChangesAsync(ct);

            return new AuthTokenResponse(
                TokenType: "Bearer",
                AccessToken: accessToken,
                AccessTokenExpiresAtUtc: accessExp,
                RefreshToken: refreshTokenRaw,
                RefreshTokenExpiresAtUtc: refreshExp,
                User: new UserIdentityDto(user.Id, user.Email, user.FullName, user.IsActive, roles, permissions)
            );
        }

        private string CreateJwt(User user, IReadOnlyList<string> roles, IReadOnlyList<string> permissions, DateTime expiresUtc)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwt.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("name", user.FullName)
        };

            foreach (var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));
            foreach (var p in permissions) claims.Add(new Claim("perm", p));

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresUtc,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string CreateSecureRandomToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        private static string HashToken(string raw)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(raw);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
