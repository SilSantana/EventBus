using System;

namespace Demo.EntitiesDto.Models.Booking
{
    public class BookingDetailsResponseDto
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public string Destination { get; set; }
        public int TotalDays { get; set; }
        public DateTime StartDate { get; set; }
    }
}
