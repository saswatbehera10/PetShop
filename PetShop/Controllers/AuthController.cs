using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using PetShop.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private PetShopDbContext context;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IUserRepo userRepo;

        public AuthController(PetShopDbContext context, IConfiguration configuration, IMapper mapper, IUserRepo userRepo)
        {
            this.context = context;
            this.configuration = configuration;
            this.mapper = mapper;
            this.userRepo = userRepo;

        }
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginDTO loginModel)
        {

            var user = context.Users.FirstOrDefault(x => x.Email == loginModel.Email);
            var userRole = context.Roles.FirstOrDefault(x => x.RoleID == user.RoleID);

            if (user is null)
                return Unauthorized("Invalid Username or Password!");

            string hashedPassword = HashPassword(loginModel.Password);
            if (BCrypt.Net.BCrypt.Verify(loginModel.Password, hashedPassword))
            {

                int expirationMinutes = int.Parse(configuration["JWT:ExpirationMinutes"]);
                var token = JWT.GenerateToken(new Dictionary<string, string> {
                { ClaimTypes.Role, user.Role.RoleName  },
                { "RoleId", user.Role.RoleID.ToString() },
                { JwtClaimTypes.PreferredUserName, user.Name },
                { JwtClaimTypes.Id, user.UserID.ToString() },
                { JwtClaimTypes.Email, user.Email}
            }, configuration["JWT:Key"]);

                return Ok(new AddAuthResponseDTO { token = token, UserName = user.Name, role = userRole.RoleID, expirationMinutes = expirationMinutes });
            }
            else
            {
                return Unauthorized("Invalid Username or Password");
            }
        }
        [Route("Registration")]
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Create(UserRegisterDTO userRegisterDTO)
        {

            // Check if a user with the same email already exists
            var existingUser = await userRepo.GetByEmailAsync(userRegisterDTO.Email);
            if (existingUser != null)
            {
                // Return an error response indicating that the email is already registered
                return BadRequest("Email is already registered.");
            }
            //Map DTO to Domain Model           
            var userEntity = mapper.Map<User>(userRegisterDTO);
            userEntity.Password = HashPassword(userRegisterDTO.Password);



            await userRepo.CreateAsync(userEntity);
            var users = mapper.Map<UserDTO>(userEntity);

            return Ok("Registration Successful");
        }
        private string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
    }
}