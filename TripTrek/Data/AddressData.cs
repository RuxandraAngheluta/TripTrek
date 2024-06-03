using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class AddressData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country name can't be longer than 100 characters.")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City name can't be longer than 100 characters.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, ErrorMessage = "Street name can't be longer than 100 characters.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Number is required.")]
        [StringLength(20, ErrorMessage = "Number can't be longer than 20 characters.")]
        public string Number { get; set; } = null!;
    }
}
