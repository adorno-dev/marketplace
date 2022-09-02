namespace Marketplace.Web.Models
{
    public class Product
    {
        public Guid Id { get; set; }        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        
        public Guid StoreId { get; set; }
        public ushort CategoryId { get; set; }

        public Store? Store { get; set; }
        public Category? Category { get; set; }


        private string[] GetImages()
        {
            if (Directory.Exists("wwwroot/uploads/products"))
            {
                return Directory.GetFiles($"wwwroot/uploads/products/{Id.ToString()}")
                                .Select(s => s.Replace("wwwroot", ""))
                                .ToArray();
            }

            return Array.Empty<string>();
        }

        public string[] Images
        {
            get => this.GetImages();
        }

        public string GetCover()
        {
            return Images.Any() ? Images.First() : string.Empty;
        }
    }
}