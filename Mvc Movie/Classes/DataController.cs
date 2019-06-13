using DataConnectorNS;
using MvcMovie.Models;
using System;
using System.Collections.Generic;

namespace Mvc_Movie.Classes
{
    public class DataController
    {
        private List<Movie> movies;
        private List<Restriction> restrictions;

        public List<Movie> Movies
        {
            get
            {
                if (movies is null)
                {
                    movies = new List<Movie>();
                    GetMovieData();
                }

                return movies;
            }
            set
            {
                movies = value;
            }
        }

        public List<Restriction> Restrictions
        {
            get
            {
                if (restrictions is null)
                {
                    restrictions = new List<Restriction>();
                    GetRestrictionData();
                }

                return restrictions;
            }
            set
            {
                restrictions = value;
            }
        }



        public DataController() { }

        public void GetMovieData()
        {
            string[,] data = DataConnector.GetData("Movies");

            for (int i = 0; i < data.GetUpperBound(0); i++)
            {
                Movie movie = new Movie()
                {
                    ID = Convert.ToInt32(data[i, 0]),
                    Title = data[i, 1],
                    ReleaseDate = Convert.ToDateTime(data[i, 2]),
                    Price = Convert.ToDecimal(data[i, 3]),
                    Poster = data[i, 4],
                    IMDbID = data[i, 5],
                    IMDbRating = Convert.ToDecimal(data[i, 6])
                };

                Movies.Add(movie);
            }
        }

        public void GetRestrictionData()
        {
            string[,] data = DataConnector.GetData("Restrictions");

            for (int i = 0; i < data.GetUpperBound(0); i++)
            {
                Restriction restriction = new Restriction()
                {
                    ID = Convert.ToInt32(data[i, 0]),
                    ISO_3166_1 = data[i, 1],
                    Certification = data[i, 2],
                    Description = data[i, 3],
                    Order = Convert.ToInt32(data[i, 4])
                };

                Restrictions.Add(restriction);
            }
        }

        internal void AddMovie(Movie movie)
        {
            string[] data = new string[Movie.PropertyCount];

            data[0] = movie.ID.ToString();
            data[1] = movie.Title;
            data[2] = movie.ReleaseDate.ToShortDateString();
            data[3] = movie.Price.ToString();
            data[4] = movie.Poster;
            data[5] = movie.IMDbID;
            data[6] = movie.IMDbRating.ToString();

            DataConnector.InsertData("Movies");
        }
    }
}
