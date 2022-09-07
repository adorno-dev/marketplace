using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marketplace.Web.Services
{
    public abstract class ApiBaseService
    {
        protected readonly JsonSerializerOptions serializerOptions;
        protected readonly HttpClient httpClient;

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
    }
}