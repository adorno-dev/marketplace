namespace Marketplace.API.Contracts.Responses
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public UserResponse? User { get; set; }
        public IEnumerable<CartItemResponse>? Items { get; set; }
    }
}