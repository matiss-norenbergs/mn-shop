using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MN_API.Models;

namespace MN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly UserContext _context;

        public UserDataController(UserContext context)
        {
            _context = context;
        }

        // GET: api/UserData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUserDataSet()
        {
            return await _context.UserDataSet.ToListAsync();
        }

        // GET: api/UserData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserData(long id)
        {
            var userData = await _context.UserDataSet.FindAsync(id);

            if (userData == null)
            {
                return NotFound();
            }

            return userData;
        }

        // PUT: api/UserData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserData(long id, UserData userData)
        {
            if (id != userData.Id)
            {
                return BadRequest();
            }

            _context.Entry(userData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/UserData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserData>> PostUserData(UserData userData)
        {
            _context.UserDataSet.Add(userData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserData), new { id = userData.Id }, userData);
        }

        // DELETE: api/UserData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(long id)
        {
            var userData = await _context.UserDataSet.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }

            _context.UserDataSet.Remove(userData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDataExists(long id)
        {
            return _context.UserDataSet.Any(e => e.Id == id);
        }
    }
}
