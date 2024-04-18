using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travelometer.Areas.Identity.Data;
using Travelometer.Models;
using Travelometer.Models.Domain;

namespace Travelometer.Controllers
{
    [Authorize]
    public class GuestController : Controller
    {
        private readonly TravelometerDbContext mvcTravelDbContext;

        public GuestController(TravelometerDbContext mvcTravelDbContext)
        {
            this.mvcTravelDbContext = mvcTravelDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddGuestViewModel addGuestRequest)
        {
            var guest = new GuestUser()
            {   
                Id = Guid.NewGuid(),
                Name = addGuestRequest.Name,
                Email = addGuestRequest.Email,
                Phone = addGuestRequest.Phone
            };

            await mvcTravelDbContext.AddAsync(guest);
            await mvcTravelDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
    }
}
