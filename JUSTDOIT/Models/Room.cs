using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace viacinema.Models
{
    public class Room
    {
        public int Id { get; set; }
         
        [Required]
        public int roomNo { get; set; }

        [Required, Range(30, 50)]
        public int totalSeats { get; set; }     //used for entering rooms into the DB 
                                               //as different rooms can have a different # of seats

        [Required]
        public int availableSeats{ get; set; }
    }
}
