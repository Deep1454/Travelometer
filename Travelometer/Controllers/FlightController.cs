using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travelometer.Areas.Identity.Data;
using Travelometer.Models;
using Travelometer.Models.Domain;

namespace Travelometer.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly TravelometerDbContext mvcTravelDbContext;

        public FlightController(TravelometerDbContext mvcTravelDbContext)
        {
            this.mvcTravelDbContext = mvcTravelDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var filghts = await mvcTravelDbContext.Flights.ToListAsync();
            return View(filghts);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddFlightViewModel addFlightRequest)
        {

            var flight = new Flight()
            {
                Id = Guid.NewGuid(),
                Airline = addFlightRequest.Airline,
                DepartureLocation = addFlightRequest.DepartureLocation,
                ArrivalLocation = addFlightRequest.ArrivalLocation,
                DepartureTime = addFlightRequest.DepartureTime,
                ArrivalTime = addFlightRequest.ArrivalTime,
                Price = addFlightRequest.Price
            };
            
            await mvcTravelDbContext.Flights.AddAsync(flight);
            await mvcTravelDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfirmBooking()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchFlightViewModel searchViewModel)
        {
            if (!ModelState.IsValid)
            {
               
                return View("Search", searchViewModel);
            }

            
            var matchingFlights = await mvcTravelDbContext.Flights
                .Where(f => f.DepartureLocation.ToLower() == searchViewModel.DepartureLocation.ToLower()
                         && f.ArrivalLocation.ToLower() == searchViewModel.ArrivalLocation.ToLower())
                .ToListAsync();

            return View("Index", matchingFlights);
        }


      
        public IActionResult EnterEmail(Guid flightId)
        {
            ViewBag.FlightId = flightId;
            return View(flightId);
        }


        [HttpPost]
        public async Task<IActionResult> Book(string email, Guid flightId)
        {
          
            var guestUser = await mvcTravelDbContext.GuestUsers.FirstOrDefaultAsync(g => g.Email == email);

            if (guestUser == null)
            {
              
                return RedirectToAction("EnterEmail", new { flightId });
            }

          
            var guestId = guestUser.Id;

           
            var booking = new Booking
            {
                GuestId = guestId,
                FlightId = flightId
            };

            mvcTravelDbContext.Bookings.Add(booking);
            await mvcTravelDbContext.SaveChangesAsync();

           
            return RedirectToAction("ConfirmBooking");
        }

    }
}
