namespace Marketplace.Web.Models
{
    public class BillingInfo
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? CardNumber { get; set; }
        public string? NameOnCard { get; set; }
        public string? CVV { get; set; }
        public string? ExpireDate { get; set; }
    }
}
