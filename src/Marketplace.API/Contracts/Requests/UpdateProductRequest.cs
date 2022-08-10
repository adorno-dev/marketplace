namespace Marketplace.API.Contracts.Requests
{
    public class UpdateProductRequest : CreateProductRequest
    {
        public Guid Id { get; set; }
    }
}