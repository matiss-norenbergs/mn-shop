using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.Models;
using MN_Shop.Server.ModelsJson;
using MN_Shop.Server.Services;

namespace MN_Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDataController(ProductService productService, UserContext userContext) : ControllerBase
    {
        private readonly ProductService _productService = productService;

        [HttpGet]
        public ActionResult<IEnumerable<ProductDataJson>> Get()
        {
            try
            {
                var productList = _productService.GetProductList();
                if (productList == null)
                    return NotFound();

                var productJson = ProductHelper.GetProductDataJson(productList, userContext.User);

                return Ok(productJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDataJson> Get(long id)
        {
            try
            {
                var product = _productService.GetProductData(id);
                if (product == null)
                    throw new Exception("Error getting product data!");

                var productJson = ProductHelper.GetProductDataJson(product, userContext.User);

                return Ok(productJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] ProductData productData)
        {
            try
            {
                productData.Name = productData.Name.Trim();

                if (string.IsNullOrEmpty(productData.Name))
                    return BadRequest();

                var response = _productService.SetProductData(productData);
                if (response <= 0)
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
        public ActionResult Delete(long id)
        {
            try
            {
                var response = _productService.DeleteProduct(id);
                if (!response)
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
