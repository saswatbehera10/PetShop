using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.Controllers;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;

namespace PetShopTests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IUserRepo> mockUserRepo;
        private Mock<IMapper> mockMapper;
        private AuthController authController;

        [SetUp]
        public void Setup()
        {
            // Initialize mocks and controller
            mockUserRepo = new Mock<IUserRepo>();
            mockMapper = new Mock<IMapper>();
            authController = new AuthController(null, null, mockMapper.Object, mockUserRepo.Object);
        }

        [Test]
        public void Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginModel = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "password"
            };
            var user = new User
            {
                Email = "test@example.com",
                Password = "hashedPassword"
            };
            var userRole = new Role
            {
                RoleID = 1,
                RoleName = "Admin"
            };

            mockUserRepo.Setup(repo => repo.GetByEmailAsync(loginModel.Email))
                .ReturnsAsync(user);
            mockMapper.Setup(mapper => mapper.Map<Role>(It.IsAny<UserLoginDTO>()))
                .Returns(userRole);

            // Act
            var result = authController.Login(loginModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.ExecuteResultAsync);
        }

        [Test]
        public void Login_InvalidCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var loginModel = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "password"
            };

            mockUserRepo.Setup(repo => repo.GetByEmailAsync(loginModel.Email))
                .ReturnsAsync((User)null);

            // Act
            var result = authController.Login(loginModel);

            // Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result.ExecuteResultAsync);
        }

        /*[Test]
        public void Create_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password",
                Phone = "1234567890",
                RoleID = 1
            };
            var userEntity = new User
            {
                // Set properties as needed
            };

            mockUserRepo.Setup(repo => repo.GetByEmailAsync(userRegisterDTO.Email))
                .ReturnsAsync((User)null);
            mockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<UserRegisterDTO>()))
                .Returns(userEntity);

            // Act
            var result = authController.Create(userRegisterDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void Create_ExistingUser_ReturnsBadRequestResult()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password",
                Phone = "1234567890",
                RoleID = 1
            };
            var existingUser = new User
            {
                // Set properties as needed
            };

            mockUserRepo.Setup(repo => repo.GetByEmailAsync(userRegisterDTO.Email))
                .ReturnsAsync(existingUser);

            // Act
            var result = authController.Create(userRegisterDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }*/
    }
}