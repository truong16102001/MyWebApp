using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;
using MyWebApp.Data;

namespace MyWebApp.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }


        private Category GetCategoryById(int id)
        {
            try
            {
                return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var c = GetCategoryById(id);
                if (c == null) return NotFound();
                return Ok(c);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            try
            {
                var c = new Category
                {
                    CategoryName = categoryVM.CategoryName
                };
                _context.Add(c);
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Data = c
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, CategoryVM categoryEdit)
        {
            try
            {
                var p = GetCategoryById(id);
                if (p == null) return NotFound();

                if (id != p.CategoryId) return BadRequest();

                //update
                p.CategoryName = categoryEdit.CategoryName;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var p = GetCategoryById(id);
                if (p == null) return NotFound();

                if (id != p.CategoryId) return BadRequest();

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
        public IActionResult Patch(int id, CategoryVM categoryPatch)
        {
            try
            {
                var p = GetCategoryById(id);
                if (p == null) return NotFound();

                if (id != p.CategoryId) return BadRequest();

                //update only the provided fields
                if (!string.IsNullOrEmpty(categoryPatch.CategoryName))
                    p.CategoryName = categoryPatch.CategoryName;
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
