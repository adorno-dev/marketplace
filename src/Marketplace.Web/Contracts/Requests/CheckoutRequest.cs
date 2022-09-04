using Marketplace.Web.Models;

namespace Marketplace.Web.Contracts.Requests
{
    public class CheckoutRequest
    {
        public BillingInfo? BillingInfo { get; set; }
        public Cart? Cart { get; set; }
    }
}
