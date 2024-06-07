using System;
using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class TripData
    {
        public int Id { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public DateTime? DepartureDate { get; set; }

        [Required]
        public DateTime? DateOfReturn { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Budget { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PersonsNr must be at least 1.")]
        public int PersonsNr { get; set; }

        [Required]
        public int TouristSpotsId { get; set; }

        public int? UserId { get; set; }

        public int? HotelId { get; set; }

        public int? TransportId { get; set; }
        // New properties for hotel and location
        [Required]
        public string HotelName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
    }
}
