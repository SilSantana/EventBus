using Demo.Api.Extensions;
using Demo.Api.Models.Booking;
using Demo.EntitiesDto.Resources.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers.Booking
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
       
        /// <summary>
        /// Health Check
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET { }
        /// </remarks>
        /// <returns>200 HTTP status code</returns>
        [HttpGet]
        public IActionResult Health()
        {
            return new JsonResult(Ok());
        }


        /// <summary>
        /// Get Booking Details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET { 
        /// 
        ///  "BookingId": 1234
        /// 
        /// }
        /// </remarks>
        /// <returns>200 HTTP status code</returns>
        [HttpGet]
        [Route(nameof(GetBooking))]
        [ProducesResponseType(typeof(BookingDetailResult),StatusCodes.Status200OK)]
        [IsValidationActionFiltersAtribute(RouteEnums = RouteEnums.GetBookingDetails)]
        public JsonResult GetBooking([FromQuery] BookingDetailRequest model)
        {
            var response = new BookingDetailResult();
            return new JsonResult(response);
        }


    }
}
