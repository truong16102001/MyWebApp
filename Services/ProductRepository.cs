using Microsoft.Extensions.Options;
using MyWebApiApp.Models;
using MyWebApp.Data;
using MyWebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyWebApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public ProductVM Add(ProductManipulationModel productModel)
        {
            var p = new Product
            {
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
                UnitPrice = p.UnitPrice,
                CategoryVM = GetCategoryById(p.CategoryId)
            };
        }

        public void Delete(Guid id)
        {
            var p = _context.Products.SingleOrDefault(c => c.ProductId == id);
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
                UnitPrice = c.UnitPrice,
                UnitInStock = c.UnitInStock,

                CategoryVM = c.Category != null ? new CategoryVM
                {
                    CategoryId = c.Category.CategoryId,
                    CategoryName = c.Category.CategoryName
                } : null
            });
            return products.ToList();
        }

        public List<ProductVM> GetProductsByConditions(FilterOptions filterOptions)
        {
            var products = _context.Products.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filterOptions.SearchKey))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(filterOptions.SearchKey.ToLower().Trim()));
            }

            if (filterOptions.FromPrice.HasValue)
            {
                products = products.Where(p => p.UnitPrice >= filterOptions.FromPrice.Value);
            }

            if (filterOptions.ToPrice.HasValue)
            {
                products = products.Where(p => p.UnitPrice <= filterOptions.ToPrice.Value);
            }

            #endregion

            #region sort
            //Default sortbyName
            switch (filterOptions.SortBy)
            {
                case "name_desc": products = products.OrderByDescending(p => p.ProductName); break;
                case "price_asc": products = products.OrderBy(p => p.UnitPrice); break;
                case "price_desc": products = products.OrderByDescending(p => p.UnitPrice); break;
            }
            #endregion

            #region paging
            if (filterOptions.Page.HasValue && filterOptions.PageSize.HasValue)
            {
                int skip = (filterOptions.Page.Value - 1) * filterOptions.PageSize.Value;
                products = products.Skip(skip).Take(filterOptions.PageSize.Value);
            }
            #endregion

            var result = products.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                UnitPrice = p.UnitPrice,
                UnitInStock = p.UnitInStock,
                CategoryVM = p.Category != null ? new CategoryVM
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName
                } : null
            });
            return result.ToList();
        }


        public ProductVM GetById(Guid id)
        {
            var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                return new ProductVM
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    UnitInStock = product.UnitInStock,
                    UnitPrice = product.UnitPrice,
                    CategoryVM = product.Category != null ? new CategoryVM
                    {
                        CategoryId = product.Category.CategoryId,
                        CategoryName = product.Category.CategoryName
                    } : null
                };
            }
            return null;
        }

        public CategoryVM GetCategoryById(int? categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null) return new CategoryVM
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
            return null;
        }

        public ProductVM Update(ProductManipulationModel productVM)
        {
            var product = _context.Products.Find(productVM.ProductId);
            if (product == null)
            {
                return null;
            }
            product.ProductName = productVM.ProductName ?? product.ProductName;
            product.ProductDescription = productVM.ProductDescription ?? product.ProductDescription;
            product.UnitInStock = productVM.UnitInStock != 0 ? productVM.UnitInStock : product.UnitInStock;
            product.UnitPrice = productVM.UnitPrice != 0 ? productVM.UnitPrice : product.UnitPrice;
            product.CategoryId = productVM.CategoryId != 0 ? productVM.CategoryId : null;
            _context.SaveChanges();

            return new ProductVM
            {
                ProductId = productVM.ProductId,
                ProductName = productVM?.ProductName,
                ProductDescription = productVM?.ProductDescription,
                UnitInStock = productVM.UnitInStock,
                UnitPrice = productVM.UnitPrice,
                CategoryVM = GetCategoryById(productVM.CategoryId)
            };
        }
    }
}
