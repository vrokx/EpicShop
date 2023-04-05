using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EpicShopAPI.Data;
using EpicShopAPI.Models;
using EpicShopAPI.Models.DAL;
using EpicShopAPI.Models.DTO;
using EpicShopAPI.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EpicShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        //private readonly IMapper _mapper;
        //private readonly IUserService _userService;
        //private readonly IConfiguration _configuration;

        //public AuthController(IMapper mapper,IUserService userService, IConfiguration configuration)
        //{
        //    _mapper = mapper;
        //    _userService = userService;
        //    _configuration = configuration;
        //}

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public async Task<IActionResult> Register(UserDto userDto)
        //{
        //    try
        //    {
        //        var user = _mapper.Map<UserModel>(userDto);

        //        return Ok(new { UserId = user.UserId, message = "User registered successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(UserDto loginDto)
        //{
        //    try
        //    {

        //        var user = await _userService.Login(loginDto.Email, loginDto.Password);

        //        if (user == null)
        //            return BadRequest("Invalid email or password.");

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("UserId", user.UserId.ToString()),
        //                new Claim(ClaimTypes.Role, user.Role)
        //            }),
        //            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration.GetSection("JwtConfig:ExpirationInMinutes").Value)),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var token = tokenHandler.CreateToken(tokenDescriptor);

        //        return Ok(new { token = tokenHandler.WriteToken(token) });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[Authorize]
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    try
        //    {
        //        var userId = int.Parse(User.FindFirstValue("UserId"));
        //        await _userService.Logout(userId);

        //        return Ok("User logged out successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        private readonly IConfiguration _config;
        private readonly EpicShopApiDBContext _context;
        private readonly IAllRepo<UserModel> _userObj;
        private readonly ILogger<BuyerController> _logger;

        public AuthController(IConfiguration config, EpicShopApiDBContext context, IAllRepo<UserModel> user, ILogger<BuyerController> logger)
        {
            _config = config;
            _context = context;
            _userObj = user;
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public IActionResult Create(UserModel user)
        {
            try
            {
                if (_context.UserSet.Where(u => u.Email == user.Email).FirstOrDefault() != null)
                {
                    return Ok("AlreadyExist");
                }

                _context.UserSet.Add(user);
                _context.SaveChanges();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the cart item");

                // Get the inner exception message for more details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return StatusCode(500, $"An error occurred while placing order: {innerExceptionMessage}");
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {

            var CheckUser = _context.UserSet.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (CheckUser != null)
            {
                return Ok(new JwtService(_config).GenerateToken(
                    CheckUser.UserId.ToString(),
                    CheckUser.FullName,
                    CheckUser.MobileNumber,
                    CheckUser.Email,
                    CheckUser.Gender,
                    CheckUser.Password,
                    CheckUser.Role));
            }
            return Ok("Failure");

        }
    }
}
