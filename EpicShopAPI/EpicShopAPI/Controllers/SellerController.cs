﻿using EpicShopAPI.Models.DAL;
using EpicShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EpicShopAPI.Data;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _productObj.GetById(productId);
            return Ok(product);
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
            existingProduct.ProductName = product.ProductName;
            existingProduct.Image = product.Image;
            existingProduct.Price = product.Price;

            await _productObj.Update(id, existingProduct);
            return Ok(product);
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
