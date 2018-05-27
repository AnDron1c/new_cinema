using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace viacinema.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required, Range(1, 50)]
        public int SeatNo { get; set; }

        [Required]
        public int RowNo { get; set; }

        [Required]
        public int RoomNo { get; set; }

        [Required, Range(80, 120)]
        public decimal Price { get; set; } 
    }
}
