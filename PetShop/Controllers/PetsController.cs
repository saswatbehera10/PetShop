using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        private readonly PetShopDbContext _dbContext;
        private readonly IMapper _mapper;
        public PetsController(PetShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: api/pets
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPets()
        {
            var pets = await _dbContext.Pets.ToListAsync();
            //return Ok(pets);
            return Ok(pets.Select(pet => _mapper.Map<PetDTO>(pet)));
        }

        // GET: api/pets/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDTO>> GetPet(int id)
        {
            var pet = await _dbContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }
            var petDTO = _mapper.Map<PetDTO>(pet);
            return Ok(petDTO);
        }

        // POST: api/pets
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PetDTO>> CreatePet(PetDTO petCreateDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pet = _mapper.Map<Pet>(petCreateDTO);
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            var petDTO = _mapper.Map<PetDTO>(pet);
            return CreatedAtAction(nameof(GetPet), new { id = petDTO.PetID }, petDTO);
        }

        // PUT: api/pets/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePet(int id, PetDTO petUpdateDTO)
        {
            if (id != petUpdateDTO.PetID)
            {
                return BadRequest("Pet ID mismatch");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pet = await _dbContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            _mapper.Map(petUpdateDTO, pet);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/pets/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _dbContext.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _dbContext.Pets.Remove(pet);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(int id)
        {
            return _dbContext.Pets.Any(p => p.PetID == id);
        }
    }
}
