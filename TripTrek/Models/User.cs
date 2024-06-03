using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TripTrek.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public int AccountId { get; set; }
    [JsonIgnore]
    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
