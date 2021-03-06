﻿using System;
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

        public Movie Movie { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartTime { get; set; } //the start date and time of the screening

        [Required]
        public int RoomNo { get; set; }

        [Required, MaxLength(5)]
        public string ScreenType { get; set; }  //f.ex. 2D or 3D

    }
}
