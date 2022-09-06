using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.Web.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings tokenSettings;

        public TokenService(IOptions<TokenSettings> tokenSettings)
        {
            this.tokenSettings = tokenSettings.Value;
        }

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
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
    }
}