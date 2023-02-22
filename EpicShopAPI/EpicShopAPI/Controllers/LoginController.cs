//using EpicShopAPI.Data;
//using EpicShopAPI.Models.DAL;
//using EpicShopAPI.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace EpicShopAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class LoginController : Controller
//    {
//        private readonly EpicShopApiDBContext _context;
//        private readonly IAllRepo<UserModel> _userObj;

//        public LoginController(IAllRepo<UserModel> userObj)
//        {
//            _userObj = userObj;
//        }

//        [HttpPost("Register")]
//        public IActionResult Register(UserModel user)
//        {
//            _userObj.Create(user);
//            return Ok(_userObj);
//        }

//        [HttpPost("Login")]
//        public async Task<ActionResult> Login(string email, string password, UserModel user)
//        {

//            var credentials = _context.UserSet.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

//            if (credentials != null)
//            {
//                var users = await _userObj.GetAll();
//                var userid = from id in users where id.Email == email select id.UserId).ToList();
//                HttpContext.Session.SetString("UserId", userid[0]);
//                HttpContext.Session.SetString("userEmail", email);
//                HttpContext.Session.SetString("userPassword", password);

//                var uid = (int)HttpContext.Session.GetString("UserId");

//                if (email == "admin@admin.com" && password == "admin@123")
//                {
//                    FormsAuthentication.SetAuthCookie(email, false);
//                    return RedirectToAction("DisplayAllProducts", "Seller");
//                }
//                else
//                {
//                    FormsAuthentication.SetAuthCookie(email, false);
//                    return RedirectToAction("BuyerDisplayAllProduct", "Buyer", new { });
//                }
//            }
//            return RedirectToAction("Login");
//        }

//    }
//}
