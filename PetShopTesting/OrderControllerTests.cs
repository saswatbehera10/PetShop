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
    public class OrdersControllerTests
    {
        private OrdersController ordersController;
        private Mock<IMapper> mockMapper;
        private Mock<IOrderRepo> mockOrderRepo;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<IMapper>();
            mockOrderRepo = new Mock<IOrderRepo>();
            ordersController = new OrdersController(mockMapper.Object, mockOrderRepo.Object);
        }

        [Test]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            // Arrange
            int orderId = 1;
            Order deletedOrder = new Order();

            mockOrderRepo.Setup(m => m.DeleteAsync(orderId)).Returns(Task.FromResult(deletedOrder));

            // Act
            var result = await ordersController.Delete(orderId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            var noContentResult = (NoContentResult)result;
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Test]
        public async Task GetAll_WithOrders_ReturnsListOfOrderDTOs()
        {
            // Arrange
            var orders = new List<Order>();
            var orderDTOs = new List<OrderDTO>();

            mockOrderRepo.Setup(m => m.GetAllAsync()).ReturnsAsync(orders);
            mockMapper.Setup(m => m.Map<List<OrderDTO>>(orders)).Returns(orderDTOs);

            // Act
            var result = await ordersController.GetAll();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<OrderDTO>>>(result);
            var okResult = (ActionResult<IEnumerable<OrderDTO>>)result;
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
            var okObjectResult = (OkObjectResult)okResult.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.AreSame(orderDTOs, okObjectResult.Value);
        }

    }
}