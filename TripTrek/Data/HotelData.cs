using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class HotelData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Stars must be between 1 and 5.")]
        public int Stars { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "PricePerNight must be a positive value.")]
        public decimal PricePerNight { get; set; }

        public int? AddressId { get; set; }
    }
}
