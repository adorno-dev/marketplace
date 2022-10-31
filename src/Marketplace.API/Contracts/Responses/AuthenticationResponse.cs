namespace Marketplace.API.Contracts.Responses
{
    public class AuthenticationResponse
    {
        public Guid? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public IDictionary<string, string>? Errors { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}