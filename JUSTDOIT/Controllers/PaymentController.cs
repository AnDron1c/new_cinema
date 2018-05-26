using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Models;
using viacinema.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace viacinema.Controllers
{
    [Route("payment")]
    public class PaymentController : Controller
    {
        public ApplicationDbContext context;
        static HttpClient client = new HttpClient();

        public PaymentController(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            // gets screening and seat from query string in url
            int.TryParse(Request.Query["screening"], out int screeningId);
            int.TryParse(Request.Query["seat"], out int seatId);

            if (screeningId == 0 || seatId == 0) throw new ArgumentNullException("screening or seat  are null");
            Screening screening = context.Screenings.Include(s => s.Movie).SingleOrDefault(s => s.Id == screeningId);
            SeatScreening seatScreening =  context.SeatScreeningMediator.FirstOrDefault(s => s.ScreeningId == screeningId && s.SeatId == seatId);
            Seat seat = context.Seats.SingleOrDefault(s => s.Id == seatScreening.SeatId);

            if (screening == null || seat == null) throw new NullReferenceException("screening or seat are not in database");

            return View(new PaymentViewModel(screening, seat, User.Identity.GetUserId()));
        }

        [Route("thankyou")]
        public IActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(Payment payment)
        {
            bool isCardNumberValid = await ValidateCreditCardAsync(payment.CardNumber);
            if (!isCardNumberValid) ModelState.AddModelError("CardNumber", "Card number is invalid");

            Screening screening = context.Screenings.Include(s => s.Movie).SingleOrDefault(s => s.Id == payment.ScreeningId);
            Seat seat = context.Seats.SingleOrDefault(s => s.Id == payment.SeatId);

            if (!ModelState.IsValid || !isCardNumberValid)
            {
                return View("Index", new PaymentViewModel(screening, seat, User.Identity.GetUserId()));
            }

            SeatScreening seatScreening = context.SeatScreeningMediator.SingleOrDefault(s => s.ScreeningId == payment.ScreeningId && s.SeatId == payment.SeatId);

            if (seatScreening != null)
            {
                // Update seat in db to be occupied so it cannot be booked again
                seatScreening.Occupied = true;
                // add payment to db
                context.Payments.Add(payment);
                context.SaveChanges();
                // if success, redirect to ThankYou page
                return RedirectToAction("ThankYou", "Payment");
            }

            return Content("Payment was not successful!");
        }

        [HttpPost, NonAction]
        public async Task<bool> ValidateCreditCardAsync(string creditCardNumber)
        {
            // makes a POST request to our webapi (CreditCardController) to validate credit card number
            // in case of invalidity adds an model error to ModelState to display on the page
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44318/api/creditcard/validate", creditCardNumber);
            response.EnsureSuccessStatusCode();

            bool isValid = await response.Content.ReadAsAsync<bool>();
            return isValid;
        }
    }
}