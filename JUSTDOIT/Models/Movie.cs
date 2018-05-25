using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace viacinema.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(100)]
        public string Title { get; set; }

        [Required, Range(1800, 18000)]
        public int DurationInSeconds { get; set; }

        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required, MaxLength(25), RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Genre { get; set; }

        [Range(0, 5)]
        public byte Rating { get; set; } 

        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
