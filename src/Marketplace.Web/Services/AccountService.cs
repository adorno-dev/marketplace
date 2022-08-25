using System.Text.Json;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Contracts.Responses;
using Marketplace.Web.Services.Contracts;

namespace Marketplace.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        public AccountService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");
            
            serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<SignInResponse?> SignIn(SignInRequest request)
        {
            SignInResponse? response = null;

            using (var post = await httpClient.PostAsJsonAsync<SignInRequest>("api/authentication/signin", request))
            {
                if (post.IsSuccessStatusCode)
                {
                    response = await post.Content.ReadFromJsonAsync<SignInResponse>();
                }
            }

            return response;
        }

        public async Task<bool> SignUp(SignUpRequest request)
        {
            using (var post = await httpClient.PostAsJsonAsync<SignUpRequest>("api/authentication/signup", request))
                return post.IsSuccessStatusCode;
        }
    }
}