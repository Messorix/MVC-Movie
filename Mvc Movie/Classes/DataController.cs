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
        private List<Genre> genres;

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

        public List<Genre> Genres
        {
            get
            {
                if (genres is null)
                {
                    genres = new List<Genre>();
                    GetGenreData();
                }

                return genres;
            }
            set
            {
                genres = value;
            }
        }

        public DataController() { }

        public void GetMovieData()
        {
            string[,] data = DataConnector.GetData("Movies");

            for (int i = 0; i < data.GetUpperBound(0) + 1; i++)
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

                string[,] restrictions = DataConnector.GetDataWithWhere(string.Format("SELECT r.* FROM Restrictions AS r, MovieRestrictions AS mr WHERE mr.RestrictionID = r.RestrictionID AND mr.MovieID = '{0}'", movie.ID), "MovieRestrictions", "MovieID", movie.ID);

                for (int y = 0; y < restrictions.GetUpperBound(0) + 1; y++)
                {
                    Restriction restriction = new Restriction()
                    {
                        ID = Convert.ToInt32(restrictions[y, 0]),
                        ISO_3166_1 = restrictions[y, 1],
                        Certification = restrictions[y, 2],
                        Description = restrictions[y, 3],
                        Order = Convert.ToInt32(restrictions[y, 4])
                    };

                    movie.Restrictions.Add(restriction);
                }

                string[,] genres = DataConnector.GetDataWithWhere(string.Format("SELECT g.* FROM Genres AS g, MovieGenres AS mg WHERE mg.GenreID = g.GenreID AND mg.MovieID = '{0}'", movie.ID), "MovieGenres", "MovieID", movie.ID);

                for (int y = 0; y < genres.GetUpperBound(0) + 1; y++)
                {
                    Genre genre = new Genre()
                    {
                        ID = Convert.ToInt32(genres[y, 0]),
                        Name = genres[y, 1]
                    };

                    movie.Genres.Add(genre);
                }

                movies.Add(movie);
            }
        }

        public void GetRestrictionData()
        {
            string[,] data = DataConnector.GetData("Restrictions");

            for (int i = 0; i < data.GetUpperBound(0) + 1; i++)
            {
                Restriction restriction = new Restriction()
                {
                    ID = Convert.ToInt32(data[i, 0]),
                    ISO_3166_1 = data[i, 1],
                    Certification = data[i, 2],
                    Description = data[i, 3],
                    Order = Convert.ToInt32(data[i, 4])
                };

                restrictions.Add(restriction);
            }
        }

        public void GetGenreData()
        {
            string[,] data = DataConnector.GetData("Genres");

            for (int i = 0; i < data.GetUpperBound(0) + 1; i++)
            {
                Genre genre = new Genre()
                {
                    ID = Convert.ToInt32(data[i, 0]),
                    Name = data[i, 1]
                };

                genres.Add(genre);
            }
        }

        internal int AddMovie(Movie movie)
        {
            int result = 0;

            int ID = 0;

            foreach (Movie m in Movies)
            {
                if (m.ID >= ID)
                    ID = m.ID + 1;
            }

            movie.ID = ID;

            Dictionary<string, dynamic> movieData = new Dictionary<string, dynamic>
            {
                { "MovieID", movie.ID },
                { "Title", movie.Title },
                { "Release Date", movie.ReleaseDate.ToString("yyyy-MM-dd") },
                { "Price", movie.Price },
                { "Poster", movie.Poster },
                { "IMDbID", movie.IMDbID },
                { "IMDbRating", movie.IMDbRating }
            };

            DataConnector.InsertData("Movies", movieData);

            foreach (Restriction r in movie.Restrictions)
            {
                Dictionary<string, dynamic> movieRestData = new Dictionary<string, dynamic>
                {
                    { "MovieID", movie.ID },
                    { "RestrictionID", r.ID }
                };

                DataConnector.InsertData("MovieRestrictions", movieRestData);
            }

            foreach (Genre g in movie.Genres)
            {
                Dictionary<string, dynamic> movieGenreData = new Dictionary<string, dynamic>
                {
                    { "MovieID", movie.ID },
                    { "GenreID", g.ID }
                };

                DataConnector.InsertData("MovieGenres", movieGenreData);
            }

            return result;
        }

        internal int AddRestriction(Restriction restriction)
        {
            int ID = 0;

            foreach (Restriction r in Restrictions)
            {
                if (r.ID >= ID)
                    ID = r.ID + 1;
            }

            restriction.ID = ID;

            Dictionary<string, dynamic> restrictionData = new Dictionary<string, dynamic>
            {
                { "RestrictionID", restriction.ID },
                { "Locale", restriction.ISO_3166_1 },
                { "Certification", restriction.Certification },
                { "Description", restriction.Description },
                { "Sequence", restriction.Order }
            };

            return DataConnector.InsertData("Restrictions", restrictionData);
        }

        internal int AddGenre(Genre genre)
        {
            Dictionary<string, dynamic> genreData = new Dictionary<string, dynamic>
            {
                { "GenreID", genre.ID },
                { "Genre", genre.Name }
            };

            return DataConnector.InsertData("Genres", genreData);
        }
    }
}
