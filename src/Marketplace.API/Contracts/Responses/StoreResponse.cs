namespace Marketplace.API.Contracts.Responses
{
    public class StoreResponse
    {
        public Guid Id { get; set; }
        public DateTime Joined { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Profile { get; set; }
        public string? Politics { get; set; }
        public string? Logo { get; set; }
        public string? Banner { get; set; }
        public UserResponse? User { get; set; }

        public IList<ProductResponse>? Items { get; set; }
    }

    public sealed class StorePaginatedResponse : StoreResponse
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
    }
}