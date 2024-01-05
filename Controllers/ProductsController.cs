using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;
using MyWebApp.Data;
using MyWebApp.Models;
using MyWebApp.Services;

namespace MyWebApp.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository context)
        {
            _productRepository = context;
        }

       

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_productRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("filter")]
        public IActionResult GetProductsByConditions([FromQuery] FilterOptions filterOptions)
        {
            try
            {
                var result = _productRepository.GetProductsByConditions(filterOptions);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var c = _productRepository.GetById(id);
                if (c == null) return NotFound();
                return Ok(c);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public IActionResult Create(ProductManipulationModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = _productRepository.GetCategoryById(productModel.CategoryId);
                if (category == null)
                {
                    return BadRequest("Invalid CategoryId");
                }

                var p = _productRepository.Add(productModel);
                return Ok(p);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Guid id, ProductManipulationModel productEdit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra điều kiện
            if (id != productEdit.ProductId)
            {
                return BadRequest("ProductId in the URL does not match ProductId in the model.");
            }
            try
            {
         
                var result = _productRepository.Update(productEdit);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var existingProduct = _productRepository.GetById(id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }       
    }
}
