using MovieRental.Models;
using System.Data.Entity;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        // Initiating DbContext --------------------------

        public ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //------------------------------------------------

       
        public ActionResult New()
        {
            var genreDetail = _context.Genres.ToList();
            var movieModel = new MovieFormViewModel
            {
                
                Genres = genreDetail
            };
            return View("MovieForm", movieModel);
        }

        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


        // GET: Movies/Random
        public ActionResult Details(int movieId)
        {
            ////---Hard coded movie list----------------
            ////var movieId = id;
            //var movies = new List<Movie> {
            //    new Movie(){Id=1, Name="Shrek"},
            //    new Movie(){Id=2, Name="WallE"}
            //};
            //Movie movie = movies.ElementAt(id - 1);
            ////----------------------------------------
            var selectedMovie = _context.Movies.SingleOrDefault(m => m.Id == movieId);
            if (selectedMovie == null)
                HttpNotFound();
            var movie = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Movie = selectedMovie
            };
            return View("MovieForm", movie);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(movie.Id ==0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieDbId = _context.Movies.Single(m => m.Id == movie.Id);
                movieDbId.Name = movie.Name;
                movieDbId.ReleaseDate = movie.ReleaseDate;
               // movieDbId.DateAdded = movie.DateAdded;
                movieDbId.GenreId = movie.GenreId;
                movieDbId.NumberInStock = movie.NumberInStock;                
            }
            try
            {
                _context.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
           

            return RedirectToAction("Index", "Movies");
        }

        //GET: Movies/index
        public ActionResult Index(int? pageIndex)
        {
            //// Hard Coded movie list --------
            // if (!pageIndex.HasValue)
            //     pageIndex = 1;
            // var movies = new List<Movie> {
            //     new Movie(){Id=1, Name="Shrek"},
            //     new Movie(){Id=2, Name="WallE"}
            // };
            // var movieList = new MovieListViewModel()
            // {
            //     Movies = movies
            // };
            // // ---------------------------

            var movieList = _context.Movies.Include(m => m.Genre);

            return View(movieList);
        }

    }
}