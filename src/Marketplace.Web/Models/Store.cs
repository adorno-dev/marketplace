using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Web.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public IEnumerable<ushort>? Categories { get; set; }
        public Guid UserId { get; set; }        
    }
}