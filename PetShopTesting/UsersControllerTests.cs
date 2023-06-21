using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using PetShop.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetShopTesting.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController userController;
        private Mock<IMapper> mockMapper;
        private Mock<IUserRepo> mockUserRepo;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<IMapper>();
            mockUserRepo = new Mock<IUserRepo>();
            userController = new UsersController(mockMapper.Object, mockUserRepo.Object);
        }
    

        [Test]
        public async Task get_user_by_id_existing()
        {
            // Arrange
            var userId = 1;
            var user = new User { UserID = userId, Name = "John Doe", Email = "johndoe@example.com" };

            mockUserRepo.Setup(m => m.GetByIdAsync(userId)).ReturnsAsync(user);
            mockMapper.Setup(m => m.Map<UserDTO>(user)).Returns(new UserDTO());

            // Act
            var result = await userController.GetById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOf<UserDTO>(okResult.Value);
        }

        [Test]
        public async Task get_user_by_id_nonexistent()
        {
            // Arrange
            var userId = 1;

            mockUserRepo.Setup(m => m.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await userController.GetById(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = (NotFoundResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}