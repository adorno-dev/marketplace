using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Contracts.Responses;

namespace Marketplace.Web.Services.Contracts
{
    public interface IAccountService
    {
        Task<SignInResponse?> SignIn(SignInRequest request);

        Task<bool> SignUp(SignUpRequest request);
    }
}