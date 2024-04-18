namespace Travelometer.Models.Domain
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string? Airline { get; set; }
        public string? DepartureLocation { get; set; }
        public string? ArrivalLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }

    public class Hotel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public decimal Price { get; set; }
        public string? Amenities { get; set; }
    }

    public class CarRental
    {
        public Guid Id { get; set; }
        public string? CarModel { get; set; }
        public string? RentalCompany { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
    }

    public class Booking
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public Guid HotelId { get; set; }
        public Guid CarRentalId { get; set; }
        public Guid GuestId { get; set; }
    }

    public class GuestUser
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
