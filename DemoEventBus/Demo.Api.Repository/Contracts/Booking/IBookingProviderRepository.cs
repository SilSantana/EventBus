using Demo.Api.Repository.Models.Booking;
using Refit;
using System.Threading.Tasks;

namespace Demo.Api.Repository.Contracts.Booking
{
    public interface IBookingProviderRepository
    {
        [Get("/Booking/Get/Details?BookingId={BookingId}")]
        Task<GetBookingResponse> GetBookingDetails([AliasAs("BookingId")] int bookingId);

    }
}
