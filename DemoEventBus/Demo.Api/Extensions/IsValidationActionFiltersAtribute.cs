using Demo.Api.Models.Booking;
using Demo.EntitiesDto.Resources.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Demo.Api.Extensions
{
    public class IsValidationActionFiltersAtribute : ActionFilterAttribute
    {

        public RouteEnums RouteEnums { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            switch (RouteEnums)
            {
                case RouteEnums.GetBookingDetails:
                    BookingDetailRequest request = context.ActionArguments["model"] as BookingDetailRequest;

                    if (request.BookingId == 14)
                    {
                        context.Result = new JsonResult(   
                            new BookingDetailResult()                            
                            {  
                                BookingId = 14,
                                ClientId = 100,
                                Destination = "Morro de São Paulo - Bahia - Brazil",  
                                TotalDays = 10,
                                StartDate = DateTime.UtcNow                              
                            }                         
                        );
                    }
                    break;
                case RouteEnums.Destinations:
                    break;
                default:
                    break;
            }

        }








    }
}
