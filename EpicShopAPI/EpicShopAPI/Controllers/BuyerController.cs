using EpicShopAPI.Data;
using EpicShopAPI.Models;
using EpicShopAPI.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EpicShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyerController : Controller
    {
        private readonly EpicShopApiDBContext _context;
        private readonly IAllRepo<ProductModel> _productObj;
        private readonly IAllRepo<UserModel> _userObj;
        private readonly IAllRepo<CartModel> _cartObj;
        private readonly IAllRepo<CategoryModel> _categoryObj;
        private readonly IAllRepo<OrderModel> _orderObj;
        private readonly IAllRepo<PreviousOrdersModel> _previousOrdersObj;
        private readonly IAllRepo<RoleModel> _roleObj;
        private readonly IAllRepo<WalletModel> _walletObj;

        public BuyerController(IAllRepo<ProductModel> productObj, IAllRepo<UserModel> userObj, IAllRepo<CartModel> cartObj, IAllRepo<CategoryModel> categoryObj, IAllRepo<OrderModel> orderObj, IAllRepo<PreviousOrdersModel> previousOrdersObj, IAllRepo<RoleModel> roleObj, IAllRepo<WalletModel> walletObj)
        {
            this._productObj = productObj;
            this._userObj = userObj;
            this._cartObj = cartObj;
            this._categoryObj = categoryObj;
            this._orderObj = orderObj;
            this._previousOrdersObj = previousOrdersObj;
            this._roleObj = roleObj;
            this._walletObj = walletObj;
        }

        [HttpGet("DisplayAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productObj.GetAll();
            return Ok(products);
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(string qty, int? Id)
        {
            ProductModel po = await _productObj.GetById(Convert.ToInt32(Id));
            CartModel co = new CartModel();
            co.ProductModel_ProductId = po.ProductId;
            co.productname = po.ProductName;
            co.price = (int)po.Price;
            co.Quantity = Convert.ToInt32(qty);
            co.TotalAmount = co.price * co.Quantity;

            List<CartModel> li = new List<CartModel>();

            if (TempData["cart"] == null)
            {
                li.Add(co);
                TempData["cart"] = li;
            }
            else
            {
                List<CartModel> li2 = TempData["cart"] as List<CartModel>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.CartId == co.ProductModel_ProductId)
                    {
                        item.Quantity = co.Quantity;
                        item.TotalAmount = co.TotalAmount;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(co);
                }

                TempData["cart"] = li2;
            }
            TempData.Keep();

            _cartObj.Create(co);

            return Ok();
        }
    }
}
