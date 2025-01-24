using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CosmeticsStore.Repositories.Context;
using CosmeticsStore.Repositories.Interfaces;
using CosmeticsStore.Repositories.Models.Domain;
using CosmeticsStore.Repositories.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CosmeticsStore.Repositories.Implements;

public class TokenRepository : ITokenRepository
{
    private const int RefreshTokenExpirationTime = 60 * 60 * 24 * 7;
    private const int AccessTokenExpirationTime = 60 * 60 * 30;
    private readonly IConfiguration _configuration;
    private readonly CosmeticsStoreDbContext _dbContext;

    public TokenRepository(IConfiguration configuration, CosmeticsStoreDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public string GenerateJwtToken(ApplicationUser user, string role)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.UserName!),
            new Claim(ClaimTypes.Role, role)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentException("Key cannot be null")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddSeconds(AccessTokenExpirationTime),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user)
    {
        var rt = await GetRefreshTokenAsync(user.Id);
        if (rt != null)
        {
            _dbContext.RefreshTokens.Remove(rt);
        }

        var newRt = CreateNewRefreshToken(user);
        await _dbContext.RefreshTokens.AddAsync(newRt);
        await _dbContext.SaveChangesAsync();
        return newRt;
    }

    private RefreshToken CreateNewRefreshToken(ApplicationUser user)
    {
        return new RefreshToken()
        {
            UserId = user.Id,
            ExpirationTime = TimeConverter.ToVietNamTime(DateTimeOffset.Now.AddSeconds(RefreshTokenExpirationTime))
        };
    }

    private async Task<RefreshToken?> GetRefreshTokenAsync(string userId)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
    }

    public async Task<string?> GetUserIdByRefreshTokenAsync(Guid refreshTokenId)
    {
        var rt = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == refreshTokenId);
        if (rt == null || rt.ExpirationTime < TimeConverter.ToVietNamTime(DateTimeOffset.Now))
        {
            return null;
        }

        return rt.UserId;
    }
}