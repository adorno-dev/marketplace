namespace Marketplace.API.Contracts.Responses
{
    public class CategoryResponse
    {
        public ushort Id { get; set; }
        public string? Name { get; set; }
        public ushort? ParentId { get; set; }
        public CategoryResponse? Parent { get; set; }
    }
}