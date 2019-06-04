using MvcMovie.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Movie.Classes
{
    public class RestTalker
    {
        private static List<Restriction> _restrictions = new List<Restriction>();

        private static IRestResponse GetResponse(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            return client.Execute(request);
        }

        internal static List<Restriction> GetRestrictionsFromAPI()
        {
            List<Restriction> returnable = new List<Restriction>();

            IRestResponse response = GetResponse("https://api.themoviedb.org/3/certification/movie/list?api_key=f2f0cf300300555b253b3e509fae67ae");

            if (response.IsSuccessful)
            {
                dynamic result = JsonConvert.DeserializeObject(response.Content);

                foreach (dynamic lang in result.certifications)
                {
                    if (lang.Name == "NL" || lang.Name == "US")
                    {
                        foreach (dynamic cert in lang.Value)
                        {
                            Restriction rest = new Restriction
                            {
                                Certification = cert.certification,
                                Description = cert.meaning,
                                Order = cert.order,
                                ISO_3166_1 = lang.Name
                            };

                            returnable.Add(rest);
                        }
                    }
                }
            }

            return returnable;
        }

        internal static Movie GetIMDB(Movie movie)
        {
            string title = movie.Title.Replace(" ", "%20");
            title = title.Replace(":", "%3A");
            
            IRestResponse response = GetResponse("https://api.themoviedb.org/3/search/movie?include_adult=false&page=1&query=" + title + "&language=en-US&api_key=f2f0cf300300555b253b3e509fae67ae");

            if (response.IsSuccessful)
            {
                dynamic resultGeneral = JsonConvert.DeserializeObject(response.Content);
                var resultMovie = resultGeneral.results[0];

                List<Certifier> restrictions = new List<Certifier>();

                string imdbID = GetIMDBID(Convert.ToInt32(resultMovie.id), ref restrictions);
                string imdbRating = resultMovie.vote_average;
                string poster = "https://image.tmdb.org/t/p/w185" + resultMovie.poster_path;
                DateTime release = resultMovie.release_date;

                movie.IMDbID = imdbID;
                movie.IMDbRating = imdbRating;
                movie.Poster = poster;
                movie.ReleaseDate = release;
            }

            return movie;
        }

        internal static string GetIMDBID(int id, ref List<Certifier> restrictions)
        {
            string returnable = "";

            if (_restrictions.Count == 0)
            {
                GetRestrictionsFromDB();
            }

            IRestResponse response = GetResponse("https://api.themoviedb.org/3/movie/" + id + "?api_key=f2f0cf300300555b253b3e509fae67ae&append_to_response=release_dates");

            if (response.IsSuccessful)
            {
                dynamic result = JsonConvert.DeserializeObject(response.Content);

                foreach (dynamic cert in result.release_dates.results)
                {
                    if (cert.iso_3166_1 == "NL" || cert.iso_3166_1 == "US")
                    {
                        Restriction restrict = null;

                        foreach (Restriction r in _restrictions)
                        {
                            if (r.ISO_3166_1 == cert.iso_3166_1.Value)
                            {
                                if (string.IsNullOrEmpty(cert.release_dates[0].certification.Value))
                                {
                                    restrict = new Restriction
                                    {
                                        Certification = "N/A",
                                        Description = "Not available",
                                        ISO_3166_1 = cert.iso_3166_1.Value,
                                        Order = -1
                                    };
                                }
                                else if (r.Certification == cert.release_dates[0].certification.Value)
                                {
                                    restrict = r;
                                    break;
                                }
                            }
                        }

                        Certifier rest = new Certifier()
                        {
                            Certification = restrict
                        };

                        restrictions.Add(rest);
                    }
                }

                returnable = Convert.ToString(result.imdb_id).Remove(0, 2);
            }

            return returnable;
        }

        private static void GetRestrictionsFromDB()
        {
            var x = from r in new MovieDBContext().Restrictions
                    select r;

            foreach (var rest in x.ToList())
            {
                _restrictions.Add(rest);
            }
        }
    }
}