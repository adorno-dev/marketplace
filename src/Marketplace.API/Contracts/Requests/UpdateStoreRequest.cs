namespace Marketplace.API.Contracts.Requests
{
    public class UpdateStoreRequest : CreateStoreRequest
    {
        public ushort Id { get; set; }
    }
}