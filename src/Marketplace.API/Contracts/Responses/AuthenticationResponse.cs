namespace Marketplace.API.Contracts.Responses
{
    public class AuthenticationResponse
    {        
        public IDictionary<string, string>? Errors { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}