namespace Marketplace.API.Contracts.Responses
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public UserResponse? User { get; set; }
        public IList<CartItemResponse>? Items { get; set; }
    }

    public class CartPaginatedResponse : CartResponse
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
    }
}