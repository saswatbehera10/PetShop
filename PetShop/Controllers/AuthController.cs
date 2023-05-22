using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly PetShopDbContext _dbContext;
        // private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
       // private readonly IPasswordHasher _passwordHasher;

        public AuthController(PetShopDbContext pedbcontext, IConfiguration configuration)
        {
            _dbContext=pedbcontext;
            //_userService = userService;
            _configuration = configuration;
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

        /*[AllowAnonymous]
        [HttpPost("login")]

        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            
            var user = await _dbContext(userDTO.Email, userDTO.Password);
            
            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Name), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtSecretKey").Value));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }*/
    }
}