using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
