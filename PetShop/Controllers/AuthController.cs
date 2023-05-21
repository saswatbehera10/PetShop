using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetShop.DataAccessLayer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO userDto)
        {
            // Validate user registration data

            // Check if user with the provided email already exists
            if (await _userService.UserExists(userDto.Email))
            {
                return BadRequest("User with the provided email already exists.");
            }

            // Create a new user entity from the DTO
            var user = new User
            {
                Email = userDto.Email,
                // Set other user properties
                // ...
            };

            // Register the user
            var registeredUser = await _userService.Register(user, userDto.Password);

            // Return the registered user or a success message
            return Ok(registeredUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDto)
        {
            // Authenticate user
            var authenticatedUser = await _userService.Authenticate(userDto.Email, userDto.Password);

            // Check if authentication was successful
            if (authenticatedUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(authenticatedUser);

            // Return the token
            return Ok(new { Token = token });
        }

        [HttpGet("test")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdminAuthorization()
        {
            // This endpoint can only be accessed by users with the "Admin" role
            return Ok("You have successfully accessed the admin-only endpoint.");
        }

        private string GenerateJwtToken(User user)
        {
            // Create claims for the user, including their ID, email, roles, etc.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                // Add other claims like roles, permissions, etc.
                // ...
            };

            // Get the secret key from configuration
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Generate token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:TokenExpirationDays"])),
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}