namespace Marketplace.API.Contracts.Responses
{
    public class FavoriteResponse
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string? Name { get; set; }
        // public string? Description { get; set; }
        // public string? Store { get; set; }
        public string? Screenshoot { get; set; }
        public decimal Price { get; set; }

        public string[]? GetScreenshoots()
        {
            string[]? screenshoots = null;

            try
            {
                screenshoots = Directory.GetFiles($"wwwroot/uploads/products/{Id}")
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
                screenshoot = Directory.GetFiles($"wwwroot/uploads/products/{Id}")
                                       .Select(s => s.Replace("wwwroot", "https://localhost:5000"))
                                       .FirstOrDefault();
            }
            catch (IOException) {}

            return screenshoot;
        }
    }
}