using MyWebApiApp.Models;
using MyWebApp.Models;

namespace MyWebApp.Services
{
    public interface IProductRepository
    {
        List<ProductVM> GetAll();
        ProductVM GetById(string id);
        ProductVM Add(ProductModel productModel);
        void Update(ProductVM productVM);
        void Delete(string id);
    }
}
