namespace Travelometer.Models
{
    public class AddBookingViewModel
    {
        public Guid FlightId { get; set; }
        public string? Email { get; set; } = null;
    }
}
