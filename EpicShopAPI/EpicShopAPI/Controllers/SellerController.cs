using EpicShopAPI.Models.DAL;
using EpicShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EpicShopAPI.Data;

namespace EpicShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
        private readonly EpicShopApiDBContext _context;
        private readonly IAllRepo<ProductModel> _productObj;

        public SellerController(IAllRepo<ProductModel> productObj)
        {
            _productObj = productObj;
        }

        [HttpGet("DisplayAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productObj.GetAll();
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> Create(ProductModel product)
        {
            await _productObj.Create(product);
            return Ok(product);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update(int id, ProductModel product)
        {
            var existingProduct = await _productObj.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productObj.Update(id, product);
            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await _productObj.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productObj.Delete(id);
            return NoContent();
        }
    }
}
