using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.Controllers;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopTesting.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AuthController authController;
        private PetShopDbContext dbContext;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IUserRepo> mockUserRepo;
        private Mock<IMapper> mockMapper;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            dbContext = new PetShopDbContext(options);

            mockConfiguration = new Mock<IConfiguration>();
            mockUserRepo = new Mock<IUserRepo>();
            mockMapper = new Mock<IMapper>();

            authController = new AuthController(dbContext, mockConfiguration.Object, mockMapper.Object, mockUserRepo.Object);
        }

        [Test]
        public void login_valid_credentials()
        {
            // Arrange
            var loginModel = new UserLoginDTO { Email = "johndoe@example.com", Password = "password123" };
            var user = new User { UserID = 1, Name = "John Doe", Email = "johndoe@example.com", Password = "hashed_password", Phone = "9876543210", RoleID = 1 };

            mockUserRepo.Setup(m => m.GetByEmailAsync(loginModel.Email)).ReturnsAsync(user);
            mockMapper.Setup(m => m.Map<AddAuthResponseDTO>(user)).Returns(new AddAuthResponseDTO());

            // Act
            var result = authController.Login(loginModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOf<AddAuthResponseDTO>(okResult.Value);
        }

        [Test]
        public void login_invalid_credentials()
        {
            // Arrange
            var loginModel = new UserLoginDTO { Email = "johndoe@example.com", Password = "password123" };

            mockUserRepo.Setup(m => m.GetByEmailAsync(loginModel.Email)).ReturnsAsync((User)null);

            // Act
            var result = authController.Login(loginModel);

            // Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
            var unauthorizedResult = (UnauthorizedObjectResult)result;
            Assert.AreEqual(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
        }

        [Test]
        public async Task create_valid_user()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                Phone = "9876543210",
                RoleID = 1
            };
            var userEntity = new User { UserID = 1, Name = "John Doe", Email = "johndoe@example.com", Password = "hashed_password", Phone = "9876543210", RoleID = 1 };

            mockUserRepo.Setup(m => m.GetByEmailAsync(userRegisterDTO.Email)).ReturnsAsync((User)null);
            mockMapper.Setup(m => m.Map<User>(userRegisterDTO)).Returns(userEntity);
            mockUserRepo.Setup(m => m.CreateAsync(It.IsAny<User>())).ReturnsAsync(userEntity);
            mockMapper.Setup(m => m.Map<UserDTO>(userEntity)).Returns(new UserDTO());

            // Act
            var result = await authController.Create(userRegisterDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("Registration Successful", okResult.Value);
        }

        [Test]
        public async Task create_existing_user()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123"
            };
            var existingUser = new User { UserID = 1, Name = "John Doe", Email = "johndoe@example.com", Password = "hashed_password" };

            mockUserRepo.Setup(m => m.GetByEmailAsync(userRegisterDTO.Email)).ReturnsAsync(existingUser);

            // Act
            var result = await authController.Create(userRegisterDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.AreEqual("Email is already registered.", badRequestResult.Value);
        }
    }
}