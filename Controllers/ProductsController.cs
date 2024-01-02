using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;
using MyWebApp.Data;

namespace MyWebApp.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        private Product GetProductById(string id)
        {
            try
            {
                return _context.Products.FirstOrDefault(pr => pr.ProductId == Guid.Parse(id));
            }
            catch (Exception)
            {
                return null;
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var p = GetProductById(id);
                if (p == null) return NotFound();
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            var p = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = productVM.ProductName,
                ProductDescription = productVM.ProductDescription,
                UnitPrice = productVM.UnitPrice,
                UnitInStock = productVM.UnitInStock,
                CategoryId = productVM.CategoryId != null ? productVM.CategoryId : null
            };
            _context.Add(p);
            _context.SaveChanges();
            return Ok(new
            {
                Success = true,
                Data = p
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, ProductVM productEdit)
        {
            try
            {
                var p = GetProductById(id);
                if (p == null) return NotFound();

                if (Guid.Parse(id) != p.ProductId) return BadRequest();

                //update
                p.ProductName = productEdit.ProductName;
                p.ProductDescription = productEdit.ProductDescription;
                p.UnitPrice = productEdit.UnitPrice;
                p.UnitInStock = productEdit.UnitInStock;

                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Data = p
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var p = GetProductById(id);
                if (p == null) return NotFound();

                if (Guid.Parse(id) != p.ProductId) return BadRequest();

                //delete
                _context.Remove(p);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(string id, ProductVM productPatch)
        {
            try
            {
                var p = GetProductById(id);
                if (p == null) return NotFound();

                if (Guid.Parse(id) != p.ProductId) return BadRequest();

                //update only the provided fields
                if (!string.IsNullOrEmpty(productPatch.ProductName))
                    p.ProductName = productPatch.ProductName;

                if (!string.IsNullOrEmpty(productPatch.ProductDescription))
                    p.ProductDescription = productPatch.ProductDescription;

                if (productPatch.UnitPrice != null)
                {
                    p.UnitPrice = productPatch.UnitPrice;
                }

                if (productPatch.UnitInStock != null)
                {
                    p.UnitInStock = productPatch.UnitInStock;
                }

                _context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = p
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
