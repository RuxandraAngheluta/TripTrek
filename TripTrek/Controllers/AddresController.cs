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
    public class AddressController : ControllerBase
    {
        private readonly PiiContext _context;

        public AddressController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.Addresses
                .Include(a => a.Hotels)
                .Include(a => a.TouristSpots)
                .ToListAsync();
        }

        // GET: api/Address/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressData>> GetAddress(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Hotels)
                .Include(a => a.TouristSpots)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            var addressData = new AddressData
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                Number = address.Number
            };

            return Ok(addressData);
        }


        // POST: api/Address
        [HttpPost]
        public async Task<ActionResult<List<Address>>> AddAddress([FromBody] AddressData addressData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = new Address
            {
                Id=addressData.Id,
                Country = addressData.Country,
                City = addressData.City,
                Street = addressData.Street,
                Number = addressData.Number
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(await _context.Addresses.ToListAsync());
        }

        // PUT: api/Address/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> UpdateAddress(int id, [FromBody] AddressData addressData)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            address.Country = addressData.Country;
            address.City = addressData.City;
            address.Street = addressData.Street;
            address.Number = addressData.Number;

            await _context.SaveChangesAsync();

            return Ok(address);
        }

        // DELETE: api/Address/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Address>>> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok(await _context.Addresses.ToListAsync());
        }
    }
}
