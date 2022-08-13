using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Marketplace.API.Models;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings tokenSettings;
        private readonly UserManager<User> userManager;

        private string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSettings.GetSecret()),
                SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Subject = claimsIdentity
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public TokenService(IOptions<TokenSettings> tokenSettings, UserManager<User> userManager)
        {
            this.tokenSettings = tokenSettings.Value;
            this.userManager = userManager;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return GenerateToken(new ClaimsIdentity(claims));
        }

        public string GenerateToken(IEnumerable<Claim> claims) => GenerateToken(new ClaimsIdentity(claims));

        public string GenerateRefreshToken()
        {
            using var generator = RandomNumberGenerator.Create();

            var random = new byte[32];

            generator.GetBytes(random);

            return Convert.ToBase64String(random);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var parameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(tokenSettings.GetSecret()),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            var claimsPrincipal = handler.ValidateToken(token, parameters, out var securityToken);

            if (securityToken is not JwtSecurityToken securityJwtToken || !
                securityJwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token.");
            
            return claimsPrincipal;
        }

        public string GetUserIdFromRequest(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.First().Split(" ")[1];

            var claims = GetClaimsPrincipalFromExpiredToken(token);

            return claims.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public void GenerateToken(User user, out string token) => token = GenerateToken(user).Result;

        public void GenerateToken(IEnumerable<Claim> claims, out string token) => token = GenerateToken(claims);

        public void GenerateRefreshToken(out string refreshToken) => refreshToken = GenerateRefreshToken();

        public void GetClaimsPrincipalFromExpiredToken(string token, out ClaimsPrincipal claimsPrincipal) 
            => claimsPrincipal = GetClaimsPrincipalFromExpiredToken(token);
    }
}