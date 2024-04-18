using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travelometer.Areas.Identity.Data;
using Travelometer.Models;
using Travelometer.Models.Domain;

namespace Travelometer.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly TravelometerDbContext mvcTravelDbContext;

        public HotelController(TravelometerDbContext mvcTravelDbContext)
        {
            this.mvcTravelDbContext = mvcTravelDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hotels = await mvcTravelDbContext.Hotels.ToListAsync();
            return View(hotels);

        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddHotelViewModel addHotelRequest)
        {

            var hotel = new Hotel()
            {
                Id = Guid.NewGuid(),
                Name = addHotelRequest.Name,
                Location = addHotelRequest.Location,
                Price = addHotelRequest.Price,
                Amenities = addHotelRequest.Amenities
            };

            await mvcTravelDbContext.Hotels.AddAsync(hotel);
            await mvcTravelDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
