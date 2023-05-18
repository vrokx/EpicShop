using EpicShopAPI.Data;
using EpicShopAPI.Models;
using EpicShopAPI.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EpicShopAPI.Models.DTO;
using Azure.Messaging;
using System.Net.Mail;
using System.Net;

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
        private readonly IProduct product1;

        public BuyerController(IAllRepo<ProductModel> productObj, IAllRepo<UserModel> userObj, IAllRepo<CartModel> cartObj, ILogger<BuyerController> logger, EpicShopApiDBContext context, IProduct product)
        {
            this._productObj = productObj;
            this._userObj = userObj;
            this._cartObj = cartObj;
            _logger = logger;
            _context = context;
            product1 = product;
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
        public async Task<IActionResult> AddToCart(int productId, int qty, [FromBody] CartModel cartItem)
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
                cartItem.ProductModel_ProductId = productId;
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
                    return NotFound(new { message = $"Cart item with ID {cartId} not found" });
                }

                await _cartObj.Delete(cartId);

                return Ok(new { message = $"Cart item with ID {cartId} has been removed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the cart item");
                return StatusCode(500, new { message = "An error occurred while removing the cart item" });
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
        public async Task<IActionResult> CreateOrRetrieveWalletAsync(int userId)
        {
            try
            {
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

        [HttpPost("wallet/addbalance")]
        public async Task<IActionResult> AddBalanceAsync([FromBody] AddBalanceDto addBalanceDto)
        {
            try
            {
                var userId = addBalanceDto.UserId;
                var amount = addBalanceDto.Amount;

                // Retrieve the user's wallet
                var wallet = await _context.WalletSet.FirstOrDefaultAsync(w => w.UserModel_UserId == userId);

                if (wallet == null)
                {
                    return NotFound();
                }
                if(amount > 0)
                {
                    wallet.CurrentBalance += amount;
                    _context.WalletSet.Update(wallet);
                    await _context.SaveChangesAsync();

                    return Ok(wallet);
                }
                // Add the balance to the wallet
                else
                {
                    return BadRequest("Enter Amount Greater Then 0");
                }

                // Return the updated wallet
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding balance to wallet");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("PaymentMode")]
        public async Task<IActionResult> PaymentMode(string mode, int grandTotal, OrderModel order)
        {
            try {
                if (mode == "wallet")
                {
                    var userId = 2;

                    var wallet = await _context.WalletSet.FirstOrDefaultAsync(w => w.UserModel_UserId == userId);
                    wallet.CurrentBalance -= grandTotal;
                    _context.WalletSet.Update(wallet);
                    await _context.SaveChangesAsync();

                    order.OrderDate = DateTime.Now;
                    order.ModeOfPayment = mode;
                    order.OrderStatus = "Confirmed";
                    order.AmountPaid = grandTotal;
                    _context.OrderSet.Update(order);
                    await _context.SaveChangesAsync();

                    return Ok(order);
                }
                else
                {
                    order.OrderDate = DateTime.Now;
                    order.ModeOfPayment = mode;
                    order.OrderStatus = "Confirmed";
                    order.AmountPaid = 0;
                    _context.OrderSet.Update(order);
                    await _context.SaveChangesAsync();

                    return Ok(order);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the cart item");

                // Get the inner exception message for more details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"An error occurred while placing order: {innerExceptionMessage}");
            }
        }

        [HttpGet("OrderDetails")]
        public async Task<IActionResult> OrderDetails()
        {
            try
            {
                var order = await _context.OrderSet.OrderBy(i => i.OrderId).LastOrDefaultAsync();
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the cart item");

                // Get the inner exception message for more details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"An error occurred while placing order: {innerExceptionMessage}");
            }
        }

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail(int userId)
        {
            try
            {
                var loggedInUser = await _userObj.GetById(userId);
                var userEmail = loggedInUser.Email;
                var order = await _context.OrderSet.OrderBy(i => i.OrderId).LastOrDefaultAsync();

                MailMessage mm = new MailMessage("radhakrishna36495@gmail.com", userEmail);
                mm.Subject = $"Order details for your Order id : {order.OrderId}";
                mm.Body = $"Order id : {order.OrderId},\n Order Date : {order.OrderDate},\n Amount Paid : {order.AmountPaid},\n Mode Of Payment : {order.ModeOfPayment},\n Order Status : {order.OrderStatus}";
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;


                NetworkCredential nc = new NetworkCredential("radhakrishna36495@gmail.com", "iufedzbfhqlpypdl");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = nc;
                smtp.Send(mm);

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the cart item");

                // Get the inner exception message for more details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"An error occurred while placing order: {innerExceptionMessage}");
            }
        }

        [HttpGet("GetAllProductsSp")]
        public async Task<IActionResult> GetAllProductsSp()
        {
            var allProducts = await product1.GetAllProductsSp();
            return Ok(allProducts);
        }

        //[HttpPost("Search")]
        //public ActionResult Search(string searchString)
        //{
        //    try
        //    {
        //        var det = _context.ProductSet.Where(d => d.ProductName.ToUpper().Contains(searchString.ToUpper())).ToList();

        //        return Ok(det);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}

    }
}
