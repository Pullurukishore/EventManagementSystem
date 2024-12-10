using EventManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EventManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly EventManagementContext _db;

        public BookingController(EventManagementContext db)
        {
            _db = db;
        }

        [HttpPost]
        public JsonResult BookEvent([FromBody] BookingRequest request)
        {
            // Get the UserId from session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue || userId.Value == 0)
            {
                return Json(new { success = false, message = "Please log in to book an event." });
            }

            // Check if the EventId exists in the Events table
            var eventExists = _db.Events.Any(e => e.EventId == request.EventId);
            if (!eventExists)
            {
                return Json(new { success = false, message = "Event not found." });
            }

            // Check if the user has already booked the event
            var alreadyBooked = _db.Bookings.Any(b => b.UserId == userId.Value && b.EventId == request.EventId);
            if (alreadyBooked)
            {
                return Json(new { success = false, message = "You have already booked this event." });
            }

            // Create a new booking if the event exists, the user is logged in, and the event is not already booked
            var booking = new Booking
            {
                UserId = userId.Value,
                EventId = request.EventId,
                BookingDate = DateTime.Now
            };

            // Add the booking to the database
            _db.Bookings.Add(booking);

            try
            {
                _db.SaveChanges();  // Save changes to the database
                return Json(new { success = true, message = "Event booked successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Booking failed. Please try again later.", error = ex.Message });
            }
        }

    }

    public class BookingRequest
    {
        public int EventId { get; set; }
    }
}
