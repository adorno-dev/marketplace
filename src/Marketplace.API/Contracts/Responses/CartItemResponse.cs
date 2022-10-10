namespace Marketplace.API.Contracts.Responses
{
    public class CartItemResponse
    {
        public Guid Id { get; set; }        
        public Guid? CartId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }

        public string? Description { get; set; }
        public ushort Quantity { get; set; }
        public decimal Price { get; set; }

        public string? Screenshoot { get; set; }
        
        //public CartResponse? Cart { get; set; }
        //public ProductResponse? Product { get; set; }

        public string[]? GetScreenshoots()
        {
            string[]? screenshoots = null;

            try
            {
                screenshoots = Directory.GetFiles($"wwwroot/uploads/products/{ProductId}")
                                        .Select(s => s.Replace("wwwroot", "https://localhost:5000"))
                                        .ToArray();
            }
            catch (IOException) {}

            return screenshoots;
        }

        public string? GetScreenshoot()
        {
            string? screenshoot = null;

            try
            {
                screenshoot = Directory.GetFiles($"wwwroot/uploads/products/{ProductId}")
                                       .Select(s => s.Replace("wwwroot", "https://localhost:5000"))
                                       .FirstOrDefault();
            }
            catch (IOException) {}

            return screenshoot;
        }
    }
}