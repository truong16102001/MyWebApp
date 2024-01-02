namespace MyWebApp.Data
{
    public enum OrderStatus
    {
        New = 0, Success = 1, Cancel = -1
    }
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime ShippedTime { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public Guid? UserId { get; set; }
        public string? Receiver { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }


        public User User { get; set; }

        //relationship
        public ICollection<OrderDetails> OrderDetails { get; set; }


        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

    }
}
