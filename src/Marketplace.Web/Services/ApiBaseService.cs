using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marketplace.Web.Services
{
    public abstract class ApiBaseService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        protected ApiBaseService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");
            
            serializerOptions = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
            };
        }

        public HttpClient Api { get => httpClient; }
    }
}