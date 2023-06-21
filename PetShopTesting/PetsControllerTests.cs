using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.Controllers;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetShop.UnitTests.Controllers
{
    [TestFixture]
    public class PetsControllerTests
    {
        private PetsController petController;
        private Mock<IMapper> mockMapper;
        private Mock<IPetRepo> mockPetRepo;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<IMapper>();
            mockPetRepo = new Mock<IPetRepo>();
            petController = new PetsController(mockMapper.Object, mockPetRepo.Object);
        }

        [Test]
        public async Task GetAll_ReturnsListOfPets()
        {
            // Arrange
            var pets = new List<Pet> { new Pet(), new Pet() };

            mockPetRepo.Setup(m => m.GetAllAsync()).ReturnsAsync(pets);
            mockMapper.Setup(m => m.Map<List<PetDTO>>(pets)).Returns(new List<PetDTO>());

            // Act
            var result = await petController.GetAll();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<PetDTO>>>(result);
            var actionResult = (ActionResult<IEnumerable<PetDTO>>)result;
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var okObjectResult = (OkObjectResult)actionResult.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.IsInstanceOf<List<PetDTO>>(okObjectResult.Value);
        }

        [Test]
        public async Task GetById_WithValidId_ReturnsPet()
        {
            // Arrange
            int petId = 1;
            var pet = new Pet();

            mockPetRepo.Setup(m => m.GetByIdAsync(petId)).ReturnsAsync(pet);
            mockMapper.Setup(m => m.Map<PetDTO>(pet)).Returns(new PetDTO());

            // Act
            var result = await petController.GetById(petId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.IsInstanceOf<PetDTO>(okObjectResult.Value);
        }

        [Test]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            // Arrange
            int petId = 1;
            var pet = new Pet();

            mockPetRepo.Setup(m => m.DeleteAsync(petId)).ReturnsAsync(pet);

            // Act
            var result = await petController.Delete(petId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            var noContentResult = (NoContentResult)result;
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}