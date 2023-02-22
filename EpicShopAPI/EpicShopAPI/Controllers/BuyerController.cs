using EpicShopAPI.Data;
using EpicShopAPI.Models;
using EpicShopAPI.Models.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace EpicShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyerController : Controller
    {
        private readonly EpicShopApiDBContext _context;
        private readonly ILogger<BuyerController> _logger;
        private readonly IAllRepo<ProductModel> _productObj;
        private readonly IAllRepo<UserModel> _userObj;
        private readonly IAllRepo<CartModel> _cartObj;
        private readonly IAllRepo<CategoryModel> _categoryObj;
        private readonly IAllRepo<OrderModel> _orderObj;
        private readonly IAllRepo<PreviousOrdersModel> _previousOrdersObj;
        private readonly IAllRepo<RoleModel> _roleObj;
        private readonly IAllRepo<WalletModel> _walletObj;

        public BuyerController(IAllRepo<ProductModel> productObj, IAllRepo<UserModel> userObj, IAllRepo<CartModel> cartObj, IAllRepo<CategoryModel> categoryObj, IAllRepo<OrderModel> orderObj, IAllRepo<PreviousOrdersModel> previousOrdersObj, IAllRepo<RoleModel> roleObj, IAllRepo<WalletModel> walletObj, ILogger<BuyerController> logger)
        {
            this._productObj = productObj;
            this._userObj = userObj;
            this._cartObj = cartObj;
            this._categoryObj = categoryObj;
            this._orderObj = orderObj;
            this._previousOrdersObj = previousOrdersObj;
            this._roleObj = roleObj;
            this._walletObj = walletObj;
            _logger = logger;
        }

        [HttpGet("BuyerDisplayAllProduct")]
        public async Task<IActionResult> BuyerDisplayAllProduct()
        {
            try
            {
                var products = await _productObj.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId,int qty ,[FromBody] CartModel cartItem)
        {
            try
            {
                // Get the product from the database
                var product = _productObj.GetById(productId);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                // Set the properties of the cart item
                cartItem.productname = product.Result.ProductName;
                cartItem.ProductModel_ProductId = product.Result.ProductId;
                cartItem.price = (int)product.Result.Price;
                cartItem.TotalAmount = cartItem.price * qty;
                cartItem.Quantity = qty;

                // Add the cart item to the repository
                await _cartObj.Create(cartItem);

                return Ok(cartItem);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception message
                _logger.LogError(ex, "An error occurred while saving the cart item");

                // Get the inner exception message for more details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"An error occurred while saving the cart item: {innerExceptionMessage}");
            }
        }
        [HttpDelete("RemoveCart")]
        public async Task<IActionResult> RemoveCart(int cartId)
        {
            try
            {
                var cartItem = await _cartObj.GetById(cartId);

                if (cartItem == null)
                {
                    return NotFound($"Cart item with ID {cartId} not found");
                }

                await _cartObj.Delete(cartId);

                return Ok($"Cart item with ID {cartId} has been removed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the cart item");
                return StatusCode(500, "An error occurred while removing the cart item");
            }
        }

        [HttpGet("ViewCart")]
        public async Task<IActionResult> ViewCart()
        {
            try
            {
                // Retrieve all cart items from the database
                var cartItems = await _cartObj.GetAll();

                // Return the cart items as a list of CartModel objects
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving cart items");
                return StatusCode(500, "An error occurred while retrieving cart items");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserModel updatedUser)
        {
            try
            {
                // Retrieve the existing user from the database
                var existingUser = await _userObj.GetById(userId);
                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                // Update the properties of the existing user
                existingUser.FullName = updatedUser.FullName;
                existingUser.Email = updatedUser.Email;
                existingUser.Gender = updatedUser.Gender;
                existingUser.MobileNumber = updatedUser.MobileNumber;

                // Update the user in the database
                await _userObj.Update(userId, existingUser);

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user");
                return StatusCode(500, "An error occurred while updating the user");
            }
        }

        [HttpPost("wallet")]
        public async Task<IActionResult> CreateOrRetrieveWalletAsync()
        {
            try
            {
                // Get the user ID from the authenticated user's claims
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Check if the user already has a wallet
                var wallet = await _context.WalletSet.FirstOrDefaultAsync(w => w.UserModel_UserId == userId);

                if (wallet != null)
                {
                    // User already has a wallet, return it
                    return Ok(wallet);
                }

                // Create a new wallet for the user
                var newWallet = new WalletModel
                {
                    UserModel_UserId = userId,
                    CurrentBalance = 0
                };

                _context.WalletSet.Add(newWallet);
                await _context.SaveChangesAsync();

                // Return the newly created wallet
                return Ok(newWallet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating/retrieving wallet for user");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //[HttpPost("AddBalance")]
        //public async Task<IActionResult> AddBalance()
        //{
        //    try
        //    {
        //        var id = (int)HttpContext.Session.GetInt32("UserId");

        //        var credentials = await _walletObj.GetAll();
        //        var wallet = credentials.FirstOrDefault();

        //        return View(wallet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

        //[HttpPost("AddBalance")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddBalance(WalletModel collection)
        //{
        //    try
        //    {
        //        await _walletObj.Update((int)HttpContext.Session.GetInt32("UserId"), collection);

        //        return RedirectToAction("Checkout");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

    }
}
