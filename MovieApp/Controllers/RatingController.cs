using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MovieApp.Controllers
{
    public class RatingController : Controller
    {
        MovieContext db = new MovieContext();

        // GET: Rating
        public ActionResult Index()
        {
            return View();
        }
    }
}