using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace MovieApp.Controllers
{
    public class TvShowsController : Controller
    {
        MovieContext db = new MovieContext();

        // GET: TvShows
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Loaddata()
        {
            var tvshowList = db.TvShows.ToList<TvShow>();
            return Json(new { data = tvshowList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShow tvShow = db.TvShows.Find(id);
            if (tvShow == null)
            {
                return HttpNotFound();
            }
            ViewBag.TvShowId = id.Value;

            var comments = db.TvShowComments.Where(d => d.TvShowId == (id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.TvShowComments.Where(d => d.TvShowId == (id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.TvRating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(tvShow);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var tvshowid = Convert.ToInt32(form["TvShow_ID"]);
            var rating = Convert.ToInt32(form["Rating"]);

            TvShowComment tvComment = new TvShowComment()
            {
                TvShowId = tvshowid,
                TvComments = comment,
                TvRating = rating,
            };

            db.TvShowComments.Add(tvComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Tvshows", new { id = tvshowid });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TvShow tvshow)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.TvShows.Add(tvshow);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tvshow);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            TvShow tvshow = db.TvShows.Find(id);
            if (tvshow == null)
                return HttpNotFound();
            return View(tvshow);
        }
        [HttpPost]
        public ActionResult Edit(TvShow tvshow)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(tvshow).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details","Tvshows", new {id = tvshow.TvShow_ID });
                }
                return View(tvshow);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            TvShow tvshow = db.TvShows.Find(id);
            if (tvshow == null)
                return HttpNotFound();

            return View(tvshow);
        }
        [HttpPost]
        public ActionResult Delete(int? id, TvShow tvsho)
        {
            try
            {
                TvShow tvshow = new TvShow();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    tvshow = db.TvShows.Find(id);
                    if (tvshow == null)
                        return HttpNotFound();

                    db.TvShows.Remove(tvshow);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tvshow);
            }
            catch
            {
                return View();
            }
        }
    }
}