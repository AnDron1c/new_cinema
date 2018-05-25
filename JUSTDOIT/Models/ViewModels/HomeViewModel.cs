using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using viacinema.Models;

namespace viacinema.ViewModels
{
    public class HomeViewModel
    {
        public List<Movie> Movies { get; set; }

        public HomeViewModel(List<Movie> Movies)
        {
            this.Movies = Movies;
        }
    }
}
