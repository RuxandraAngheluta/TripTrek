using System;
using System.Collections.Generic;

namespace TripTrek.Models;

public partial class Suggestion
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public string? Pro { get; set; }

    public string? Contra { get; set; }

    public int Rating { get; set; }

    public int Userid { get; set; }
}
