using System;
using System.Collections.Generic;

namespace TripTrek.Models;

public partial class Address
{
    public int Id { get; set; }

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<TouristSpot> TouristSpots { get; set; } = new List<TouristSpot>();
}
