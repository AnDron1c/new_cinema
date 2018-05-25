using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Data;
using viacinema.Models;
using viacinema.ViewModels;

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
            int.TryParse(Request.Query["screening"], out int screeningId);
            int.TryParse(Request.Query["seat"], out int seatNo);

            if (screeningId == 0 || seatNo == 0) throw new ArgumentNullException("screening or seat  are null");
            Screening screening = context.Screenings.SingleOrDefault(s => s.Id == screeningId);
            Console.WriteLine(screening.Id);
            SeatScreening seatScreening =  context.SeatScreeningMediator.FirstOrDefault(s => s.ScreeningId == screeningId && s.SeatNo == seatNo);
            Seat seat = context.Seats.SingleOrDefault(s => s.Id == seatScreening.SeatId);

            if (screening == null || seat == null) throw new NullReferenceException("screening or seat are not in database");

            return View(new PaymentViewModel(context, screeningId, seatNo, seat.Price));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(Payment payment)
        {
            // consider using public API: https://www.bincodes.com/api-creditcard-checker/
            bool isCardNumberValid = await ValidateCreditCardAsync(payment.CardNumber);
            if (!isCardNumberValid) ModelState.AddModelError("CardNumber", "Card number is invalid");

            if (!ModelState.IsValid || !isCardNumberValid)
            {
                return View("Index", new PaymentViewModel(context, payment.ScreeningId, payment.SeatNo, payment.Amount));
            }

            context.Payments.Add(payment);
            Screening screening = context.Screenings.SingleOrDefault(s => s.Id == payment.ScreeningId);
            SeatScreening seatScreening = context.SeatScreeningMediator.FirstOrDefault(s => s.ScreeningId == payment.ScreeningId && s.SeatNo == payment.SeatNo);

            if (seatScreening != null)
            {
                seatScreening.Occupied = true;
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, NonAction]
        public async Task<bool> ValidateCreditCardAsync(string creditCardNumber)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44318/api/creditcard/validate", new { creditCardNumber });
            response.EnsureSuccessStatusCode();

            bool isValid = await response.Content.ReadAsAsync<bool>();
            return isValid;
        }
    }
}