using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

public class DashboardController : Controller
{
    private readonly EventManagementContext _db;

    // Constructor to inject EventManagementContext
    public DashboardController(EventManagementContext db)
    {
        _db = db;
    }

    // Change action name from AdminDashboard to EventStatistics
    public IActionResult EventStatistics()
    {
        try
        {
            // Get bookings grouped by EventId and count the bookings for each event
            var bookings = _db.Bookings
                .GroupBy(b => b.EventId)
                .Select(g => new { EventId = g.Key, Count = g.Count() })
                .ToList();

            // Get the events with their names
            var events = _db.Events.ToDictionary(e => e.EventId, e => e.EventName);

            // Calculate average bookings per event
            var avgBookings = _db.Bookings
                .GroupBy(b => b.EventId)
                .Select(g => new { EventId = g.Key, AvgBookings = g.Count() / (double)_db.Events.Count(e => e.EventId == g.Key) })
                .ToList();

            // Combine the booking count and average bookings with event names
            var data = bookings
                .Select(b => new
                {
                    EventName = events[b.EventId],
                    b.Count,
                    AvgBookings = avgBookings.FirstOrDefault(a => a.EventId == b.EventId)?.AvgBookings ?? 0
                })
                .ToList();

            // Return the data as JSON
            return Json(data);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, new { error = "An error occurred while fetching data." });
        }
    }
}
