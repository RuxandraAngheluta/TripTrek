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
    public class HotelController : ControllerBase
    {
        private readonly PiiContext _context;

        public HotelController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _context.Hotels
                .Include(h => h.Address)
                .Include(h => h.Trips)
                .ToListAsync();
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Address)
                .Include(h => h.Trips)
                .FirstOrDefaultAsync(h => h.Id == id);


            var hotelData = new HotelData
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Stars = hotel.Stars,
                PricePerNight = hotel.PricePerNight,
                AddressId = hotel.AddressId

            };
             if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotelData);
        }

        // POST: api/Hotel
        [HttpPost]
        public async Task<ActionResult<List<Hotel>>> AddHotel([FromBody] HotelData hotelData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotel = new Hotel
            {
                Id = hotelData.Id,
                Name = hotelData.Name,
                Stars = hotelData.Stars,
                PricePerNight = hotelData.PricePerNight,
                AddressId = hotelData.AddressId
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Hotels.ToListAsync());
        }

        // PUT: api/Hotel/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Hotel>>> UpdateHotel(int id, [FromBody] HotelData hotelData)
        {
            if (id != hotelData.Id)
            {
                return BadRequest("Hotel ID mismatch");
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            hotel.Name = hotelData.Name;
            hotel.Stars = hotelData.Stars;
            hotel.PricePerNight = hotelData.PricePerNight;
            hotel.AddressId = hotelData.AddressId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Hotels.ToListAsync());
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Hotel>>> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok(await _context.Hotels.ToListAsync());
        }
    }
}
