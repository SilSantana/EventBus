using Demo.Api.Repository.Contracts.Booking;
using Demo.EntitiesDto.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Api.Repository.Implementations.Booking
{
    public class BookingRepository : IBookingRepository
    {
        public Task<BookingDetailsResponseDto> GetBookingDetails(BookingDetailsRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
