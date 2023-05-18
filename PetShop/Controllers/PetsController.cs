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

        public PetsController(PetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/pets
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            var pets = await _dbContext.Pets.ToListAsync();
            return Ok(pets);
        }

        // GET: api/pets/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = await _dbContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        // POST: api/pets
        [HttpPost]
        public async Task<ActionResult<Pet>> CreatePet([FromBody]Pet pet)
        {
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPet), new { id = pet.PetID }, pet);
        }

        // PUT: api/pets/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePet(int id, Pet pet)
        {
            if (id != pet.PetID)
            {
                return BadRequest();
            }

            _dbContext.Entry(pet).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
