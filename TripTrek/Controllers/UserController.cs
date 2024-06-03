using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripTrek.Data;
using TripTrek.Models;

namespace TripTrek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PiiContext _context;

        public UserController(PiiContext context)
        {
            _context = context;
            
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.Account)
                .Include(u => u.Trips)
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Account)
                .Include(u => u.Trips)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userData = new UserData
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                BirthDate = user.BirthDate,
                AccountId = user.AccountId,
            };

            return Ok(userData);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser([FromBody] UserData user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
               var user1 = new User
            {
                Id= user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                BirthDate = user.BirthDate,
                AccountId=user.AccountId,
              
            };
            _context.Users.Add(user1);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser([FromBody] UserData request, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.AccountId = request.AccountId;
            user.BirthDate = request.BirthDate;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }
    }
}
