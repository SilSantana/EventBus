using System;

namespace Demo.Api.Models.Booking
{
    /// <summary>
    /// Result to get booking details
    /// </summary>
    public class BookingDetailResult
    {

        /// <summary>
        /// Booking Id identification
        /// </summary>
        public int BookingId { get; set; }

        /// <summary>
        /// Client Id identification
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Booking Destination Description
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Total of days
        /// </summary>
        public int TotalDays { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}
