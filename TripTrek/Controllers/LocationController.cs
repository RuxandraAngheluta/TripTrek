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
    public class LocationController : ControllerBase
    {
        private readonly PiiContext _context;

        public LocationController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        // GET: api/Location/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            var locationData = new LocationData
            {
                Id = location.Id,
                Country = location.Country,
                City = location.City,
                TotalRating = location.TotalRating,
                Street = location.Street
            };

            if (location == null)
            {
                return NotFound();
            }

            return Ok(locationData);
        }

        // POST: api/Location
        [HttpPost]
        public async Task<ActionResult<Location>> AddLocation([FromBody] LocationData locationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = new Location
            {
                Id=locationData.Id,
                Country = locationData.Country,
                City = locationData.City,
                TotalRating = locationData.TotalRating,
                Street = locationData.Street
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return Ok(location);
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationData locationData)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            location.Country = locationData.Country;
            location.City = locationData.City;
            location.TotalRating = locationData.TotalRating;
            location.Street = locationData.Street;

            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Location/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return location;
        }
    }
}
