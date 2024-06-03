using System;
using System.Collections.Generic;

namespace TripTrek.Models;

public partial class Trip
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public DateTime? DepartureDate { get; set; }

    public DateTime? DateOfReturn { get; set; }

    public decimal? Budget { get; set; }

    public int PersonsNr { get; set; }

    public int TouristSpotsId { get; set; }

    public int? UserId { get; set; }

    public int? HotelId { get; set; }

    public int? TransportId { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual TouristSpot TouristSpots { get; set; } = null!;

    public virtual Transport? Transport { get; set; }

    public virtual User? User { get; set; }
}
