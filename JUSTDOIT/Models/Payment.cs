using System.ComponentModel.DataAnnotations;

namespace viacinema.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int ScreeningId { get; set; }

        [Required]
        public int SeatNo { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public byte ExpiryMonth { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

        [Required]
        public int SecurityCode { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
