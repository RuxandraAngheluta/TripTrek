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
    public class AccountController : ControllerBase
    {
        private readonly PiiContext _context;

        public AccountController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // POST: api/Account
        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddAccount([FromBody] AccountData accountData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = new Account
            {
                Id = accountData.Id,
                Email = accountData.Email,
                PhoneNr = accountData.PhoneNr,
                Password = accountData.Password,
                UserId = accountData.UserId
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.ToListAsync());
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountData accountData)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            account.Email = accountData.Email;
            account.PhoneNr = accountData.PhoneNr;
            account.Password = accountData.Password;
            account.UserId = accountData.UserId;

            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }
    }
}
