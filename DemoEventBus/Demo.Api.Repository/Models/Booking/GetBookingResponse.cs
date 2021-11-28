using System;

namespace Demo.Api.Repository.Models.Booking
{
    public class GetBookingResponse
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public string Destination { get; set; }
        public int TotalDays { get; set; }
        public DateTime StartDate { get; set; }

    }
}
