using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Models;
using MN_Shop.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MN_Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDataController(ProductService productService) : ControllerBase
    {
        private readonly ProductService _productService = productService;

        [HttpGet]
        public ActionResult<IEnumerable<ProductData>> Get()
        {
            var productList = _productService.GetProductList();
            if (productList == null)
                return NotFound();

            return productList;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductData> Get(long id)
        {
            try
            {
                var product = _productService.GetProductData(id);
                if (product == null)
                    throw new Exception("Error getting product data!");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] ProductData productData)
        {
            try
            {
                productData.Name = productData.Name.Trim();

                if (string.IsNullOrEmpty(productData.Name))
                    return BadRequest();

                var resp = _productService.SetProductData(productData);
                if (resp <= 0)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var resp = _productService.DeleteProduct(id);
                if (!resp)
                    throw new Exception("Error deleting product data!");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
