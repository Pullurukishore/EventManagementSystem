using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For accessing EF features like ToList()

public class EventController : Controller
{
    // Declare the DbContext as a private readonly field
    private readonly EventManagementContext db;

    // Constructor injection to initialize the DbContext
    public EventController(EventManagementContext context)
    {
        db = context;
    }

    // Action to display the list of events
    public ActionResult Index()
    {
        var events = db.Events.ToList(); // Fetching events from the database
        return View(events); // Passing events to the view
    }

    // Action to show the form for adding a new event
    public ActionResult AddEvent()
    {
        return View();
    }

    // Action to handle the form submission for adding a new event
    [HttpPost]
    public ActionResult AddEvent(Event eventDetails)
    {
        if (ModelState.IsValid) // Checking if the model is valid
        {
            db.Events.Add(eventDetails); // Adding the event to the DbSet
            db.SaveChanges(); // Saving the changes to the database
            return RedirectToAction("Index"); // Redirecting to the event index page after saving
        }
        return View(eventDetails); // Returning the form with validation errors if any
    }
}
