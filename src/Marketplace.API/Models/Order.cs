namespace Marketplace.API.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public bool Confirmed { get; set; }

        public User? User { get; set; }
        public IList<OrderItem>? Items { get; set; }

        private Order() {}

        public Order(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Confirmed = false;
        }
    }
}