using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class LocationData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        public decimal? TotalRating { get; set; }

        public string? Street { get; set; }
    }
}
