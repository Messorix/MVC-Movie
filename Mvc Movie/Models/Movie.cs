using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }

        public string IMDbID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        public string Poster { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "IMDb Rating")]
        public string IMDbRating { get; set; }

        public List<Restriction> Restrictions { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<MovieRestriction> MovieRestrictions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Restrictions)
                        .WithMany(r => r.Movies)
                        .Map(mr =>
                                {
                                    mr.MapLeftKey("MovieID");
                                    mr.MapRightKey("RestrictionID");
                                    mr.ToTable("MovieRestriction");
                                });*/
        }
    }
}