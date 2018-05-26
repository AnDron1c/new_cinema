using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;
using viacinema.Data;
using viacinema.ViewModels;
using Microsoft.AspNet.Identity;
using viacinema.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace viacinema.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext context;

        public HomeController(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var movies = context.Movies
                .OrderByDescending(c => c.ReleaseDate)
                .OrderByDescending(c => c.Rating)
                .ToList();

            return View(new HomeViewModel(movies));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize, Route("reservations")]
        public IActionResult Reservations()
        {
            var payments = context.Payments.Include(p => p.Screening).Include(p => p.Screening.Movie).ToList();
           
            return View(new ReservationsViewModel(payments));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
