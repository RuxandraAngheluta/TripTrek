using System;
using System.Collections.Generic;

namespace TripTrek.Models;

public partial class TransportType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
