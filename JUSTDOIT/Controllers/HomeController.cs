using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;
using viacinema.Data;
using viacinema.ViewModels;

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
