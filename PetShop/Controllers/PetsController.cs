﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;
using PetShop.DataAccessLayer.Entities.Repository.Interfaces;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPetRepo petRepo;
       
        //Creating Constructor
        public PetsController(IMapper mapper, IPetRepo petRepo)
        {
            this.mapper = mapper;
            this.petRepo = petRepo;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] PetDTO petDTO)
        {
            //Map DTO to domain Model          
            var pet = mapper.Map<Pet>(petDTO);
            await petRepo.CreateAsync(pet);
            //Domain Model to DTO
            return Ok(mapper.Map<PetDTO>(pet));
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetAll()
        {
            var pet = await petRepo.GetAllAsync();

            return Ok(mapper.Map<List<PetDTO>>(pet));
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
            var pet = await petRepo.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PetDTO>(pet));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, PetUpdateDTO petUpdateDTO)
        {
            var pet = mapper.Map<Pet>(petUpdateDTO);
            pet = await petRepo.UpdateAsync(id, pet);
            if (pet == null)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<PetUpdateDTO>(pet));
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
            var pet = await petRepo.DeleteAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
