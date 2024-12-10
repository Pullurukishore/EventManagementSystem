namespace EventManagementSystem.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Comments { get; set; }
    }

}
