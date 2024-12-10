using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;


namespace EventManagementSystem.Models
{
    public class EventManagementContext : DbContext
    {
        // DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        // Constructor that accepts DbContextOptions
        public EventManagementContext(DbContextOptions<EventManagementContext> options)
            : base(options)
        {
        }
    }
}

