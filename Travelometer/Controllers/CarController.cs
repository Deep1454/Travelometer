using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travelometer.Areas.Identity.Data;
using Travelometer.Models;
using Travelometer.Models.Domain;

namespace Travelometer.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        public TravelometerDbContext MvcTraveDbContext { get; }

        public CarController(TravelometerDbContext mvcTraveDbContext)
        {
            this.MvcTraveDbContext = mvcTraveDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await MvcTraveDbContext.CarRentals.ToListAsync();
            return View(cars);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarRentalViewModel addCarRentalRequest)
        {
            var car = new CarRental()
            {
                Id = Guid.NewGuid(),
                CarModel = addCarRentalRequest.CarModel,
                RentalCompany = addCarRentalRequest.RentalCompany,
                Price = addCarRentalRequest.Price,
                Availability = addCarRentalRequest.Availability
            };

            await MvcTraveDbContext.AddAsync(car);
            await MvcTraveDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
