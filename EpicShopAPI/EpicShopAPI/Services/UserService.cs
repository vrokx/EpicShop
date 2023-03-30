using EpicShopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public interface IUserService
{
    Task<UserModel> Authenticate(string email, string password);
    Task<UserModel> Register(UserModel user);
    Task<string> GenerateJwtToken(UserModel user);
}

public class UserService : IUserService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<UserModel> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<UserModel> Authenticate(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
            return null;

        return user;
    }

    public async Task<UserModel> Register(UserModel user)
    {
        var result = await _userManager.CreateAsync(user, user.Password);

        if (!result.Succeeded)
            return null;

        return user;
    }

    public async Task<string> GenerateJwtToken(UserModel user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
