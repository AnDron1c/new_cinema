using System;
using System.ComponentModel.DataAnnotations;

namespace viacinema.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public Screening Screening { get; set; }

        [Required]
        public int ScreeningId { get; set; }

        public Seat Seat { get; set; }

        [Required]
        public int SeatId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required, MaxLength(16), MinLength(16)]
        public string CardNumber { get; set; }

        [Required, Range(1, 12)]
        public byte ExpiryMonth { get; set; }

        [Required, Range(2018, 2030)]
        public int ExpiryYear { get; set; }

        [Required, Range(100, 999)]
        public int SecurityCode { get; set; }

        public DateTime DatePaid = DateTime.Now;

        [Required]
        public decimal Amount { get; set; }
    }
}
