using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TripTrek.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public int PhoneNr { get; set; }

    public string Password { get; set; } = null!;

    public int? UserId { get; set; }
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
