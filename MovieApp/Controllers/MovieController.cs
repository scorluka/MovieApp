using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        MovieContext db = new MovieContext();

        // GET: Movie
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
    }
}