namespace Marketplace.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public bool Favorite { get; set; }
        public bool Cart { get; set; }
        
        public Guid? StoreId { get; set; }
        public ushort? CategoryId { get; set; }

        public Store? Store { get; set; }
        public Category? Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }


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