using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly PetShopDbContext _dbContext;
        private readonly IConfiguration _configuration;
        // private readonly IUserService _userService;
        // private readonly IPasswordHasher _passwordHasher;

        public AuthController(PetShopDbContext dbcontext, IConfiguration configuration)
        {
            _dbContext= dbcontext;
            _configuration = configuration;
            //_userService = userService;
            // _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = new User
            {
                Name = userRegisterDTO.Name,
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password,
                Phone = userRegisterDTO.Phone,
                RoleID=2,
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return Ok(user);

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDTO>> Login(UserLoginDTO userLoginDTO)
        {
            var user =  await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == userLoginDTO.Email && u.Password == userLoginDTO.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleID.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}