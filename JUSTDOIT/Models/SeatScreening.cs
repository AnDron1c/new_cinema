using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace viacinema.Models
{
    public class SeatScreening
    {
        public int Id { get; set; }

        [Required, Range(1, 50)]
        public int SeatNo { get; set; }

        [Required]
        public int SeatId { get; set; }

        [Required]
        public int ScreeningId { get; set; }

        [Required]
        public int RoomNo { get; set; }

        [Required]
        public Boolean Occupied { get; set; }
    }
}
