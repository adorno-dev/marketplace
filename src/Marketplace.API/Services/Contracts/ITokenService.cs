using System.Security.Claims;
using Marketplace.API.Models;

namespace Marketplace.API.Services.Contracts
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
        
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        string GetToken(HttpContext context);
        string GetUserIdFromRequest(HttpContext context);
        
        void GetUserInfo(HttpContext context, out string userId, out string userName, out string email);
        
        void GenerateToken(User user, out string token);
        void GenerateToken(IEnumerable<Claim> claims, out string token);
        void GenerateRefreshToken(out string refreshToken);
        void GetClaimsPrincipalFromExpiredToken(string token, out ClaimsPrincipal claimsPrincipal);
    }
}