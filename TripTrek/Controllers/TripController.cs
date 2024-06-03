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
    public class TripController : ControllerBase
    {
        private readonly PiiContext _context;

        public TripController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Trip
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            return await _context.Trips
               .Include(t => t.Location)
               .Include(t => t.TouristSpots)
               .Include(t => t.Hotel)
               .Include(t => t.Transport)
               .Include(t => t.User)
                .ToListAsync();
        }

        // GET: api/Trip/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripData>> GetTrip(int id)
        {
            var trip = await _context.Trips

                .Include(t => t.Location)
                .Include(t => t.TouristSpots)
                .Include(t => t.Hotel)
                .Include(t => t.Transport)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
            var tripData = new TripData
            {
                Id=trip.Id,
                LocationId=trip.LocationId,
                DepartureDate = trip.DepartureDate,
                DateOfReturn=trip.DateOfReturn,
                Budget=trip.Budget,
                PersonsNr=trip.PersonsNr,
                TouristSpotsId=trip.TouristSpotsId,
                UserId=trip.UserId,
                HotelId=trip.HotelId,
                TransportId=trip.TransportId,

            };

            if (trip == null)
            {
                return NotFound();
            }

            return tripData;
        }

        // POST: api/Trip
        [HttpPost]
        public async Task<ActionResult<List<Trip>>> AddTrip([FromBody] TripData tripData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = new Trip
            {
                Id = tripData.Id,
                LocationId = tripData.LocationId,
                DepartureDate = tripData.DepartureDate,
                DateOfReturn = tripData.DateOfReturn,
                Budget = tripData.Budget,
                PersonsNr = tripData.PersonsNr,
                TouristSpotsId = tripData.TouristSpotsId,
                UserId = tripData.UserId,
                HotelId = tripData.HotelId,
                TransportId = tripData.TransportId
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return Ok(await _context.Trips.ToListAsync());
        }

        // PUT: api/Trip/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Trip>>> UpdateTrip(int id, [FromBody] TripData tripData)
        {
            if (id != tripData.Id)
            {
                return BadRequest("Trip ID mismatch");
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            trip.LocationId = tripData.LocationId;
            trip.DepartureDate = tripData.DepartureDate;
            trip.DateOfReturn = tripData.DateOfReturn;
            trip.Budget = tripData.Budget;
            trip.PersonsNr = tripData.PersonsNr;
            trip.TouristSpotsId = tripData.TouristSpotsId;
            trip.UserId = tripData.UserId;
            trip.HotelId = tripData.HotelId;
            trip.TransportId = tripData.TransportId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Trips.ToListAsync());
        }

        // DELETE: api/Trip/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Trip>>> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return Ok(await _context.Trips.ToListAsync());
        }
    }
}
