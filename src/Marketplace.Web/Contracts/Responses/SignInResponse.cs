namespace Marketplace.Web.Contracts.Responses
{
    public class SignInResponse
    {
        public IDictionary<string, string>? Errors { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }        
    }
}