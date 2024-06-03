using System;
using System.Collections.Generic;

namespace TripTrek.Models;

public partial class TouristSpot
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal? TicketPrice { get; set; }

    public int? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
