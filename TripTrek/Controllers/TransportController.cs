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
    public class TransportController : ControllerBase
    {
        private readonly PiiContext _context;

        public TransportController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Transport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transport>>> GetTransports()
        {
            return await _context.Transports.ToListAsync();
        }

        // GET: api/Transport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transport>> GetTransport(int id)
        {
            var transport = await _context.Transports.FindAsync(id);

            if (transport == null)
            {
                return NotFound();
            }

            return transport;
        }

        // POST: api/Transport
        [HttpPost]
        public async Task<ActionResult<Transport>> AddTransport([FromBody] TransportData transportData)
        {
            var transport = new Transport
            {
                Id = transportData.Id,
                TypeId = transportData.TypeId,
                Price = transportData.Price,
                Duration = transportData.Duration,
                DepartureTime = transportData.DepartureTime,
                FromLocationId = transportData.FromLocationId,
                ToLocationId = transportData.ToLocationId
            };

            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransport), new { id = transport.Id }, transport);
        }

        // PUT: api/Transport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransport(int id, [FromBody] TransportData transportData)
        {
            if (id != transportData.Id)
            {
                return BadRequest();
            }

            var transport = await _context.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            transport.TypeId = transportData.TypeId;
            transport.Price = transportData.Price;
            transport.Duration = transportData.Duration;
            transport.DepartureTime = transportData.DepartureTime;
            transport.FromLocationId = transportData.FromLocationId;
            transport.ToLocationId = transportData.ToLocationId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Transport/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transport>> DeleteTransport(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            _context.Transports.Remove(transport);
            await _context.SaveChangesAsync();

            return transport;
        }
    }
}
