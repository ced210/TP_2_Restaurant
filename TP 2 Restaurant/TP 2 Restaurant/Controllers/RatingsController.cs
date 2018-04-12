using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IRDB.Models;

namespace TP_2_Restaurant.Controllers
{
    public class RatingsController : Controller
    {
        // GET: Ratings
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            using (var DB = new RestaurantsEntities())
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(RatingView ratingview)
        {
            using (var DB = new RestaurantsEntities())
            {
                if (ModelState.IsValid)
                {
                    Rating rating = new Rating();
                    rating = ratingview.ToRating();
                    DB.Ratings.Add(rating);
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
    }
}