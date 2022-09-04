namespace Marketplace.Web.Models
{
    public class Checkout
    {
        public BillingInfo? BillingInfo { get; set; }        
        public Cart? Cart { get; set; }
    }
}