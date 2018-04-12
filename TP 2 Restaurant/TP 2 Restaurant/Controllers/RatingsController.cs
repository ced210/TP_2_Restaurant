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

        /// <summary>
        /// Ced 12 av MOD MOD
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            using (var DB = new RestaurantsEntities())
            {
                RatingView ratingView = new RatingView();
                ratingView.Restaurant_Id = ((Restaurant)Session["CurrentRestaurant"]).Id;
                return View(ratingView);
            }
        }
        /// <summary>
        /// Ced 12 av MOD MOD
        /// </summary>
        /// <param name="ratingview"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(RatingView ratingview)
        {
            using (var DB = new RestaurantsEntities())
            {
                if (ModelState.IsValid)
                {
                    Rating rating = new Rating();
                    rating = ratingview.ToRating();
                    //le resto courrant de la page Details d'avant
                    rating.Restaurant_Id = ((Restaurant)Session["CurrentRestaurant"]).Id;

                    DB.Create(rating);
                    return RedirectToAction("Details", "Restaurants", new {id = ((Restaurant)Session["CurrentRestaurant"]).Id });
                }
                //
                ViewBag.RestaurantToEvaluate = ((Restaurant)Session["CurrentRestaurant"]).ToRestaurantView();
                return View(ratingview);
            }
        }
    }
}