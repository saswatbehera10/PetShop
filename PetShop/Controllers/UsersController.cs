using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;
using System.Data;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepo userRepo;

        //Creating Constructor
        public UsersController(IMapper mapper, IUserRepo userRepo)
        {
            this.mapper = mapper;
            this.userRepo = userRepo;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var user = await userRepo.GetAllAsync();

            return Ok(mapper.Map<List<UserDTO>>(user));
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
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserDTO>(user));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update([FromRoute] int id, UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);
            user = await userRepo.UpdateAsync(id, user);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<UserDTO>(user));
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
            var user = await userRepo.DeleteAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}