using MovieRental.Models;
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
        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


        // GET: Movies/Random
        public ActionResult Details(int id)
        {
            //var movieId = id;
            var movies = new List<Movie> {
                new Movie(){Id=1, Name="Shrek"},
                new Movie(){Id=2, Name="WallE"}
            };
            Movie movie = movies.ElementAt(id - 1);
            return View(movie);
        }

        //GET: Movies/index
        public ActionResult index(int? pageIndex)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            var movies = new List<Movie> {
                new Movie(){Id=1, Name="Shrek"},
                new Movie(){Id=2, Name="WallE"}
            };
            var movieList = new MovieListViewModel()
            {
                Movies = movies
            };

            return View(movieList);
        }
    }
}