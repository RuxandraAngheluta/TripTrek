using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class TouristSpotData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "TicketPrice must be a positive value.")]
        public decimal? TicketPrice { get; set; }

        public int? AddressId { get; set; }
    }
}
