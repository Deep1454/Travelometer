namespace Travelometer.Models
{
    public class AddCarRentalViewModel
    {
        public string? CarModel { get; set; }
        public string? RentalCompany { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
    }
}
