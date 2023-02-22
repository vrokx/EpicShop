using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EpicShopAPI.Models.DAL;
using EpicShopAPI.Models;
using EpicShopAPI.Data;
using Microsoft.AspNetCore.Authentication;

namespace RepoPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAllRepo<UserModel> _userObj;
        private readonly EpicShopApiDBContext _db;

        public LoginController(IAllRepo<UserModel> userObj, EpicShopApiDBContext db)
        {
            _userObj = userObj;
            _db = db;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserModel user)
        {
            await _userObj.Create(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(string email, string password)
        {
            var credentials = _db.UserSet.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (credentials != null)
            {
                List<UserModel> users = (await _userObj.GetAll()).ToList();
                var userid = (from id in users where id.Email == email select id.UserId).ToList();
                HttpContext.Session.SetInt32("UserId", userid[0]);
                HttpContext.Session.SetString("userEmail", email);
                HttpContext.Session.SetString("userPassword", password);

                var uid = HttpContext.Session.GetInt32("UserId");

                if (email == "admin@admin.com" && password == "admin@123")
                {
                    return Ok(credentials);
                }
                else
                {
                    return Ok(credentials);
                }
            }

            return BadRequest("Invalid login attempt.");
        }

        [Authorize]
        [HttpPost("logout")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync("EpicShopAPI");
            return Ok();
        }
    }
}
