namespace Marketplace.API.Contracts.Requests
{
    public class UpdateCategoryRequest : CreateCategoryRequest
    {
        public ushort Id { get; set; }
    }
}