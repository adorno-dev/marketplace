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
            string productImagesPath = $"wwwroot/uploads/products/{Id.ToString()}";

            if (Directory.Exists(productImagesPath))
            {
                return Directory.GetFiles(productImagesPath)
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