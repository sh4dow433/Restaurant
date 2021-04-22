using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Restaurant.BookingsAndTablesManager;
using Restaurant.DataAccess;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOptions<List<Admin>> _admins;
        private readonly IBookingsManager _bookingsManager;
        private readonly ITablesManager _tablesManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IOptions<List<Admin>> admins,
                IBookingsManager bookingsManager,
                ITablesManager tablesManager,
                IUnitOfWork unitOfWork)
        {
            _admins = admins;
            _bookingsManager = bookingsManager;
            _tablesManager = tablesManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            if (_admins.Value.Find(a => a.Username == admin.Username && a.Password == admin.Password) != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, (string)admin.Username),
                    new Claim("FullName", (string)admin.Username),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {

                    RedirectUri = "/Admin/Index",

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            ModelState.AddModelError("", "Username or password are incorrect.");
            return View(admin);
        }

        // BOOKING
        [Authorize]
        public IActionResult Booking(bool active = true)
        {
            IEnumerable<Booking> bookings;
            if (active)
            {
                bookings = _bookingsManager.GetAllActiveBookings();
            }
            else
            {
                bookings = _bookingsManager.GetAllBookings();
            }
            return View(bookings);
        }

        [Authorize]
        public IActionResult DeleteBooking(int id)
        {
            if (_bookingsManager.RemoveBookingById(id) == false)
                return NotFound();
            return RedirectToAction("Booking");
        }

        // TABLE
        [Authorize]
        public IActionResult Table()
        {
            var tables = _tablesManager.GetAllTables();
            return View(tables);
        }

        [Authorize]
        public IActionResult CreateTable()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateTable(Table table)
        {
            if (ModelState.IsValid)
            {
                if (_tablesManager.AddTable(table))
                {
                    return RedirectToAction("Table");
                }
                else
                {
                    ModelState.AddModelError("", "You cant have 2 tables with the same number");
                }
            }
            return View(table);
        }


        [Authorize]
        public IActionResult DeleteTable(int id)
        {
            if (_tablesManager.DeleteTable(id) == false)
                return NotFound();
            return RedirectToAction("Table");
        }

        // REVIEW
        [Authorize]
        public IActionResult Review()
        {
            var reviews = _unitOfWork.Reviews.GetAllOrderedDesc();
            return View(reviews);
        }

        [Authorize]
        public IActionResult DeleteReview(int id)
        {
            if (_unitOfWork.Reviews.RemoveById(id) == false)
                return NotFound();
            _unitOfWork.SaveChanges();
            return RedirectToAction("Review");
        }
    }
}
