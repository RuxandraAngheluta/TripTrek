using System;

namespace TripTrek.Data
{
    public class TransportData
    {
        public int Id { get; set; }

        public int? TypeId { get; set; }

        public decimal? Price { get; set; }

        public DateTime? Duration { get; set; }

        public DateTime? DepartureTime { get; set; }

        public int? FromLocationId { get; set; }

        public int? ToLocationId { get; set; }
    }
}
