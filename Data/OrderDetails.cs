namespace MyWebApp.Data
{
    public class OrderDetails
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int quantity { get; set; }

        public byte Discount { get; set; }

        public double Cost { get; set; }


        //relate table
        public Order Order { get; set; }

        public Product Product { get; set; }


    }
}
