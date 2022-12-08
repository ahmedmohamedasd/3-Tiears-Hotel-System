using JwtApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Data_Access_Layer;

namespace WebApi_Business_Layer.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
        private IConfiguration _config;
        private readonly HotelSystemContext _context;
        public LoginController(IConfiguration config , HotelSystemContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserModel user)
        {
            if (isUserExist(user.Username))
            {
                return BadRequest();
            }
            else
            {
                _context.UserModel.Add(user);
                _context.SaveChanges();
                var token = Generate(user);
                return Ok(token);
                
            }
        }
        private bool isUserExist(string username)
        {
            var user = _context.UserModel.FirstOrDefault(c => c.Username == username);
            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = _context.UserModel.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
