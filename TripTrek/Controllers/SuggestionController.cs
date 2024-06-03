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
    public class SuggestionController : ControllerBase
    {
        private readonly PiiContext _context;

        public SuggestionController(PiiContext context)
        {
            _context = context;
        }

        // GET: api/Suggestion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestions()
        {
            return await _context.Suggestions.ToListAsync();
        }

        // GET: api/Suggestion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Suggestion>> GetSuggestion(int id)
        {
            var suggestion = await _context.Suggestions.FindAsync(id);

            if (suggestion == null)
            {
                return NotFound();
            }

            return suggestion;
        }

        // POST: api/Suggestion
        [HttpPost]
        public async Task<ActionResult<List<Suggestion>>> AddSuggestion([FromBody] SuggestionData suggestionData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggestion = new Suggestion
            {
                Id=suggestionData.Id,
                LocationId = suggestionData.LocationId,
                Pro = suggestionData.Pro,
                Contra = suggestionData.Contra,
                Rating = suggestionData.Rating,
                Userid = suggestionData.UserId
            };

            _context.Suggestions.Add(suggestion);
            await _context.SaveChangesAsync();

            return Ok(await _context.Suggestions.ToListAsync());
        }

        // PUT: api/Suggestion/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Suggestion>> UpdateSuggestion(int id, [FromBody] SuggestionData suggestionData)
        {
            var suggestion = await _context.Suggestions.FindAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            suggestion.LocationId = suggestionData.LocationId;
            suggestion.Pro = suggestionData.Pro;
            suggestion.Contra = suggestionData.Contra;
            suggestion.Rating = suggestionData.Rating;
            suggestion.Userid = suggestionData.UserId;

            await _context.SaveChangesAsync();

            return Ok(suggestion);
        }

        // DELETE: api/Suggestion/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Suggestion>>> DeleteSuggestion(int id)
        {
            var suggestion = await _context.Suggestions.FindAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            _context.Suggestions.Remove(suggestion);
            await _context.SaveChangesAsync();

            return Ok(await _context.Suggestions.ToListAsync());
        }
    }
}
