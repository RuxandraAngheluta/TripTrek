using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class SuggestionData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "LocationId is required.")]
        public int LocationId { get; set; }

        public string? Pro { get; set; }

        public string? Contra { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
