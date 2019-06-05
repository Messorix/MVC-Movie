using Mvc_Movie.Classes;
using MvcMovie.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Mvc_Movie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext movieDB = new MovieDBContext();

        // GET: Movies
        public ActionResult Index(string movieRating, string movieGenre, string searchString, string sortOrder)
        {
            if (movieDB.Restrictions.Count() == 0)
            {
                movieDB.Restrictions.AddRange(RestTalker.GetRestrictionsFromAPI());
                movieDB.SaveChanges();
            }

            movieDB.Configuration.LazyLoadingEnabled = false;

            var GenreLst = new List<string>();

            var GenreQry = from m in movieDB.Movies
                           orderby m.Genre
                           select m.Genre;
            
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var RatingLst = new List<string>();

            var CertQry = from r in movieDB.Restrictions
                          where r.ISO_3166_1 == CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                          select r.Certification;

            RatingLst.AddRange(CertQry.Distinct());
            ViewBag.movieRating = new SelectList(RatingLst);


            var movies = from m in movieDB.Movies
                         select m;

            var restrictions = from r in movieDB.Restrictions
                         select r;


            List<Movie> x1 = movies.ToList();
            List<Restriction> y1 = restrictions.ToList();




            /*foreach (var movie in movies.ToList())
            {
                foreach (var combi in movRest.Where(x => x.MovieID.Equals(movie.ID)))
                {
                    movie.Restrictions.AddRange(restrictions.Where(y => y.ID.Equals(combi.RestrictionID)));
                }
            }

            /*var groupJoinQuery =
               from movie in movieDB.Movies
               orderby movie.ID
               join rest in movieDB.Restrictions on movie.ID equals rest.ID into prodGroup
               select new
               {
                   Category = category.Name,
                   Products = from prod2 in prodGroup
                              orderby prod2.Name
                              select prod2
               };
            */



            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            if (!string.IsNullOrEmpty(movieRating))
            {
                /*
                movies = movies.Where
                    (x => x.Certifications.Where
                        (y => y.Certification.ISO_3166_1 == CultureInfo.CurrentCulture.TwoLetterISOLanguageName).FirstOrDefault().Certification.Certification == movieRating);
                        */
            }

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            switch (sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    movies = movies.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "Price":
                    movies = movies.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    movies = movies.OrderByDescending(s => s.Price);
                    break;
                default:
                    movies = movies.OrderBy(s => s.Title);
                    break;
            }

            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieDB.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie = RestTalker.GetIMDB(movie);

                movieDB.Movies.Add(movie);
                movieDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieDB.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieDB.Entry(movie).State = EntityState.Modified;
                movieDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieDB.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = movieDB.Movies.Find(id);
            movieDB.Movies.Remove(movie);
            movieDB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                movieDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
