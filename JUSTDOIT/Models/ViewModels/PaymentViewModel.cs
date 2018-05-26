using viacinema.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using viacinema.Models;

namespace viacinema.ViewModels
{
    public class PaymentViewModel
    {

        public IEnumerable<object> Months { get; set; }

        public IEnumerable<object> Years { get; set; }

        public int ScreeningId { get; set; }

        [Required]
        public int SeatNo { get; set; }

        public string UserId { get; set; }

        [Required, Display(Name = "Name on card")]
        public string NameOnCard { get; set; }

        [Required, Display(Name = "Card number")]
        public string CardNumber { get; set; }

        [Required, Display(Name = "Expiry month")]
        public byte ExpiryMonth { get; set; }

        [Required, Display(Name = "Expiry year")]
        public byte ExpiryYear { get; set; }

        [Required, Display(Name = "Security code")]
        public int SecurityCode { get; set; }

        public decimal Amount { get; set; }

        public Movie Movie { get; set; }

        public PaymentViewModel(ApplicationDbContext context, int screeningId, int seatNo, decimal seatPrice, string userId)
        {
            ScreeningId = screeningId;
            Amount = seatPrice;
            SeatNo = seatNo;
            UserId = userId;

            Screening screening = context.Screenings.SingleOrDefault(s => s.Id == screeningId);
            Movie = context.Movies.SingleOrDefault(m => m.Id == screening.MovieId);

            List<object> months = new List<object>() {
                new { Value = 1 },
                new { Value = 2 },
                new { Value = 3 },
                new { Value = 4 },
                new { Value = 5 },
                new { Value = 6 },
                new { Value = 7 },
                new { Value = 8 },
                new { Value = 9 },
                new { Value = 10 },
                new { Value = 11 },
                new { Value = 12 }
            };

            List<object> years = new List<object>() {
                new { Value = 2018 },
                new { Value = 2019 },
                new { Value = 2020 },
                new { Value = 2021 },
                new { Value = 2022 },
                new { Value = 2023 }
            };

            Months = months;
            Years = years;
        }   
    }
}
