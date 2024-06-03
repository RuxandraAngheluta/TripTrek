using System;
using System.Collections.Generic;

namespace TripTrek.Models;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Stars { get; set; }

    public decimal PricePerNight { get; set; }

    public int? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
