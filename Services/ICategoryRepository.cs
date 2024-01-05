using MyWebApiApp.Models;
using MyWebApp.Models;

namespace MyWebApp.Services
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAll();
        CategoryVM GetById(int id);
        CategoryVM Add(CategoryManipulationModel categoryModel);
        void Update(CategoryVM categoryModel);
        void Delete(int id);
    }
}
