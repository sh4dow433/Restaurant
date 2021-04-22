using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.DataAccess;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public ReviewsController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index(int pageNumber)
        {
            int reviewsNumber = _unitOfWork.Reviews.GetNumberOfPages();
            
            if (reviewsNumber > pageNumber)
            {
               ViewBag.HasNext = true;
            }
            else
            {
                ViewBag.HasNext = false;
            }

            if (pageNumber > 0)
            {
                ViewBag.HasPrevious = true;
            }
            else
            {
                ViewBag.HasPrevious = false;
            }

            ViewBag.PageNumber = pageNumber;

            var reviews = _unitOfWork.Reviews.GetIntervalOrderedDesc(pageNumber);
            return View(reviews);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Reviews.Add(review);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }
    }
}
