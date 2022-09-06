using System.Security.Claims;

namespace Marketplace.Web.Services.Contracts
{
    public interface ITokenService
    {
        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);        
    }
}