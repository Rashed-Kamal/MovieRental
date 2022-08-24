using MovieRental.Models;
using System.Data.Entity;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == movieId);
            return View(movie);
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