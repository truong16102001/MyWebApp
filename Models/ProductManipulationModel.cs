namespace MyWebApp.Models
{
    public class ProductManipulationModel
    {
        public Guid ProductId { get; set; } // Thêm ProductId vào model để sử dụng cho cả thêm mới và chỉnh sửa
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public int? CategoryId { get; set; }

        // Thêm constructor để khởi tạo giá trị mặc định cho ProductId
        public ProductManipulationModel()
        {
            ProductId = Guid.NewGuid(); // Khởi tạo giá trị mặc định
        }
    }
}
