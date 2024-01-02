namespace MyWebApp.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public int? CategoryId { get; set; }
    }
}
