using MyWebApiApp.Models;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public ProductVM Add(ProductModel productModel)
        {
            var p = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = productModel.ProductName,
                ProductDescription = productModel.ProductDescription,
                UnitInStock = productModel.UnitInStock,
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId
            };
            _context.Add(p);
            _context.SaveChanges();

            return new ProductVM
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                UnitInStock = p.UnitInStock,
                UnitPrice = p.UnitPrice
            };
        }

        public void Delete(string id)
        {
            var p = _context.Products.SingleOrDefault(c => c.ProductId == Guid.Parse(id));
            if (p != null)
            {
                _context.Remove(p);
                _context.SaveChanges(true);
            }
        }

        public List<ProductVM> GetAll()
        {
            var products = _context.Products.Select(c => new ProductVM
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,
                ProductDescription = c.ProductDescription,
                UnitInStock = c.UnitInStock,
                UnitPrice = c.UnitPrice
            });
            return products.ToList();
        }

        public ProductVM GetById(string id)
        {
            var product = _context.Products.SingleOrDefault(p => p.ProductId == Guid.Parse(id));
            if (product != null)
            {
                return new ProductVM
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    UnitInStock = product.UnitInStock,
                    UnitPrice = product.UnitPrice
                };
            }
            return null;
        }

        public void Update(ProductVM productVM)
        {
            var product = _context.Products.SingleOrDefault(p => p.ProductId == productVM.ProductId);
            if (product != null)
            {
                product.ProductName = productVM.ProductName ?? product.ProductName;
                product.ProductDescription = productVM.ProductDescription ?? product.ProductDescription;
                product.UnitInStock = productVM.UnitInStock != 0 ? productVM.UnitInStock : product.UnitInStock;
                product.UnitPrice = productVM.UnitPrice != 0 ? productVM.UnitPrice : product.UnitPrice;
                _context.SaveChanges();
            }
        }
    }
}
