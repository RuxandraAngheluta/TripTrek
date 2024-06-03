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
    public class TransportTypeController : ControllerBase
    {
        private readonly PiiContext _context;

        public TransportTypeController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/TransportType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportType>>> GetTransportTypes()
        {
            return await _context.TransportTypes.ToListAsync();
        }

        // GET: api/TransportType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportType>> GetTransportType(int id)
        {
            var transportType = await _context.TransportTypes.FindAsync(id);

            if (transportType == null)
            {
                return NotFound();
            }

            return transportType;
        }

        // POST: api/TransportType
        [HttpPost]
        public async Task<ActionResult<TransportType>> AddTransportType([FromBody] TransportTypeData transportTypeData)
        {
            var transportType = new TransportType
            {
                Id = transportTypeData.Id,
                Name = transportTypeData.Name
            };

            _context.TransportTypes.Add(transportType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransportType), new { id = transportType.Id }, transportType);
        }

        // PUT: api/TransportType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransportType(int id, [FromBody] TransportTypeData transportTypeData)
        {
            if (id != transportTypeData.Id)
            {
                return BadRequest();
            }

            var transportType = await _context.TransportTypes.FindAsync(id);
            if (transportType == null)
            {
                return NotFound();
            }

            transportType.Name = transportTypeData.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TransportType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransportType>> DeleteTransportType(int id)
        {
            var transportType = await _context.TransportTypes.FindAsync(id);
            if (transportType == null)
            {
                return NotFound();
            }

            _context.TransportTypes.Remove(transportType);
            await _context.SaveChangesAsync();

            return transportType;
        }
    }
}
