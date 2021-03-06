﻿using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        MovieContext db = new MovieContext();

        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Loaddata()
        {
            var movList = db.Movies.ToList<Movie>();
            return Json(new { data = movList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
               return HttpNotFound();
            }
            ViewBag.MovieId = id.Value;

            var comments = db.MovieComments.Where(d => d.MovieId == (id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.MovieComments.Where(d => d.MovieId == (id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }
            
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var movieid = Convert.ToInt32(form["Movie_ID"]);
            var rating = Convert.ToInt32(form["Rating"]);

            MovieComment movComment = new MovieComment()
            {
                MovieId = movieid,
                Comments = comment,
                Rating = rating, 
            };

            db.MovieComments.Add(movComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Movie", new { id = movieid });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(movie);
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
            Movie movie = db.Movies.Find(id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(movie);
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
            Movie movie = db.Movies.Find(id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
        [HttpPost]
        public ActionResult Delete(int? id, Movie mov)
        {
            try
            {
                Movie movie = new Movie();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    movie = db.Movies.Find(id);
                    if (movie == null)
                        return HttpNotFound();

                    db.Movies.Remove(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(movie);
            }
            catch
            {
                return View();
            }
        }
    }
}