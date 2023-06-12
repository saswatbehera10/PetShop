using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using PetShop.DataAccessLayer.Entities;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOrderRepo orderRepo;

        //Creating Constructor
        public OrdersController(IMapper mapper, IOrderRepo orderRepo)
        {
            this.mapper = mapper;
            this.orderRepo = orderRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] AddOrderDTO addOrderDTO)
        {
            //Map DTO to domain Model          
            var order = mapper.Map<Order>(addOrderDTO);
            await orderRepo.CreateAsync(order);
            //Domain Model to DTO
            return Ok(mapper.Map<AddOrderDTO>(order));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll()
        {
            var order = await orderRepo.GetAllAsync();

            return Ok(mapper.Map<List<OrderDTO>>(order));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var order = await orderRepo.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<OrderDTO>(order));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, OrderDTO orderDTO)
        {
            var order = mapper.Map<Order>(orderDTO);
            order = await orderRepo.UpdateAsync(id, order);
            if (order == null)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<OrderDTO>(order));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var order = await orderRepo.DeleteAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
