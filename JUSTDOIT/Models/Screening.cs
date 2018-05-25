using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace viacinema.Models
{
    public class Screening
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [Required]
        public int RoomNo { get; set; }

        [Required, MaxLength(5)]
        public string ScreenType { get; set; }

    }
}
