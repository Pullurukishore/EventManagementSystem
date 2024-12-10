namespace EventManagementSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
