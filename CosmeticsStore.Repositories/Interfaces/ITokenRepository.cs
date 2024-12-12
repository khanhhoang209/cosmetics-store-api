using CosmeticsStore.Repositories.Models.Domain;

namespace CosmeticsStore.Repositories.Interfaces;

public interface ITokenRepository
{
    string GenerateJwtToken(ApplicationUser user, string role);
    Task<RefreshToken> GenerateRefreshToken(ApplicationUser user);
    Task<string?> GetUserIdByRefreshTokenAsync(Guid refreshTokenId);
}