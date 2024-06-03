using System;
using System.ComponentModel.DataAnnotations;

namespace TripTrek.Data
{
    public class UserData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Birth Date is required.")]
        public DateTime? BirthDate { get; set; }
        public int AccountId { get; set; }


    }
}
