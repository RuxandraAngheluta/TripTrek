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
    public class TouristSpotController : ControllerBase
    {
        private readonly PiiContext _context;

        public TouristSpotController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/TouristSpot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouristSpot>>> GetTouristSpots()
        {
            return await _context.TouristSpots
                .Include(ts => ts.Address)
                .Include(ts => ts.Trips)
                .ToListAsync();
        }

        // GET: api/TouristSpot/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TouristSpot>> GetTouristSpot(int id)
        {
            var touristSpot = await _context.TouristSpots
                .Include(ts => ts.Address)
                .Include(ts => ts.Trips)
                .FirstOrDefaultAsync(ts => ts.Id == id);
            var touristSpotData=new TouristSpotData
            {
                Id = touristSpot.Id,
                Name = touristSpot.Name,
                TicketPrice = touristSpot.TicketPrice,
                AddressId = touristSpot.AddressId

            };

            if (touristSpot == null)
            {
                return NotFound();
            }

            return Ok(touristSpotData);
        }

        // POST: api/TouristSpot
        [HttpPost]
        public async Task<ActionResult<List<TouristSpot>>> AddTouristSpot([FromBody] TouristSpotData touristSpotData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var touristSpot = new TouristSpot
            {
                Id = touristSpotData.Id,
                Name = touristSpotData.Name,
                TicketPrice = touristSpotData.TicketPrice,
                AddressId = touristSpotData.AddressId
            };

            _context.TouristSpots.Add(touristSpot);
            await _context.SaveChangesAsync();

            return Ok(await _context.TouristSpots.ToListAsync());
        }

        // PUT: api/TouristSpot/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<TouristSpot>>> UpdateTouristSpot(int id, [FromBody] TouristSpotData touristSpotData)
        {
            if (id != touristSpotData.Id)
            {
                return BadRequest("TouristSpot ID mismatch");
            }

            var touristSpot = await _context.TouristSpots.FindAsync(id);
            if (touristSpot == null)
            {
                return NotFound();
            }

            touristSpot.Name = touristSpotData.Name;
            touristSpot.TicketPrice = touristSpotData.TicketPrice;
            touristSpot.AddressId = touristSpotData.AddressId;

            await _context.SaveChangesAsync();

            return Ok(await _context.TouristSpots.ToListAsync());
        }

        // DELETE: api/TouristSpot/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TouristSpot>>> DeleteTouristSpot(int id)
        {
            var touristSpot = await _context.TouristSpots.FindAsync(id);
            if (touristSpot == null)
            {
                return NotFound();
            }

            _context.TouristSpots.Remove(touristSpot);
            await _context.SaveChangesAsync();

            return Ok(await _context.TouristSpots.ToListAsync());
        }
    }
}
