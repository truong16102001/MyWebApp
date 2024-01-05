using MyWebApiApp.Models;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Services
{
    public interface IProductRepository
    {
        List<ProductVM> GetAll();
        List<ProductVM> GetProductsByConditions(FilterOptions filterOptions);
        ProductVM GetById(Guid id);
        ProductVM Add(ProductManipulationModel productModel);
        ProductVM Update(ProductManipulationModel productVM);
        void Delete(Guid id);

        CategoryVM GetCategoryById(int? categoryId);
    }
}
