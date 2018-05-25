using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using viacinema.Models;

namespace viacinema.ViewModels
{
    public class MoreInfoViewModel
    {
        public Movie Movie { get; set; }
        public List<Screening> Screenings { get; set; }
        public List<SeatScreening> SeatScreenings { get; set; }

        public MoreInfoViewModel(Movie movie, List<Screening> scr, List<SeatScreening> seatSceen)
        {
            this.Movie = movie;
            this.Screenings = scr;
            this.SeatScreenings = seatSceen;
        }
    }
}
