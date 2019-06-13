using Mvc_Movie.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie : PropertyHandler
    {
        public Movie()
        {
            PropertyCount = 7;
        }

        public int ID { get; set; }

        public string IMDbID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        public string Poster { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "IMDb Rating")]
        public decimal IMDbRating { get; set; }
        
        public List<Genre> Genres { get; set; }

        public List<Restriction> Restrictions { get; set; }
    }
}