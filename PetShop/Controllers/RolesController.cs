using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.DataAccessLayer.Context;
using PetShop.DataAccessLayer.Entities;

namespace PetShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        private readonly PetShopDbContext _dbContext;

        public RolesController(PetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return Ok(roles);
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST: api/roles
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRole), new { id = role.RoleID }, role);
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role role)
        {
            if (id != role.RoleID)
            {
                return BadRequest();
            }

            _dbContext.Entry(role).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _dbContext.Roles.Any(r => r.RoleID == id);
        }
    }
}
