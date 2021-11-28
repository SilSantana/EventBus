using Demo.EntitiesDto.Models.Booking;
using System.Threading.Tasks;

namespace Demo.Api.Repository.Contracts.Booking
{
    public interface IBookingRepository
    {
        Task<BookingDetailsResponseDto> GetBookingDetails(BookingDetailsRequestDto request);
    }
}
