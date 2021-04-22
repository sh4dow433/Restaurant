using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BookingsAndTablesManager;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingsManager _bookingsManager;

        public BookingController(IBookingsManager bookingsManager)
        {
            _bookingsManager = bookingsManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Check an older booking
        public IActionResult Check(int id)
        {
            var booking = _bookingsManager.GetBookingById(id);
            return View(booking);
        }

        //CREATE BOOKING GET
        public IActionResult Create(int numberOfPersons, DateTime date, Hour hour)
        {
            ViewBag.NumberOfPersons = numberOfPersons;
            ViewBag.Date = date;
            ViewBag.Hour = hour;
            
            return View();
        }

        //CREATE BOOKING POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookingModel createBookingModel)
        {
            if (ModelState.IsValid)
            {
                var booking = _bookingsManager.MakeBooking(createBookingModel);
                if (booking != null)
                {
                    return RedirectToAction("Check", new { id = booking.Id});
                }
                else
                {
                    ModelState.AddModelError("", "Couldnt find a free table for that configuration! Try other date/hour/number of persons");
                    return View(createBookingModel);
                }
            }
            return View(createBookingModel);
        }
    }
}
