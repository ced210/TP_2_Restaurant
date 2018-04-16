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
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Création d'une évaluation
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            //Pour En-tete du resto courrant
            Restaurant resto = null;

            using (var DB = new RestaurantsEntities())
            {
                //En-tete du resto courrant
                resto = (Restaurant)Session["CurrentRestaurant"];
                ViewBag.RestaurantViews = resto.ToRestaurantView();

                RatingView ratingView = new RatingView();
                ratingView.Restaurant_Id = ((Restaurant)Session["CurrentRestaurant"]).Id;
                return View(ratingView);
            }
        }
        /// <summary>
        /// Post de la Création d'une évaluation
        /// </summary>
        /// <param name="ratingview">un objet RatingView à créer</param>
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
                    //Met l'objet du Restaurant présent à évaluer dans le ViewBag, pour utiliser ses informations dans l'en-tête de la page
                    ViewBag.RestaurantToEvaluate = ((Restaurant)Session["CurrentRestaurant"]).ToRestaurantView();
                    return RedirectToAction("Details", "Restaurants", new {id = ((Restaurant)Session["CurrentRestaurant"]).Id });
                }
                return View(ratingview);
            }
        }
        /// <summary>
        /// Suppression d'une Évaluation
        /// </summary>
        /// <param name="id">Id du Rating</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            int idResto;
            using (var DB = new RestaurantsEntities())
            {
                Rating rate = DB.Ratings.Find(id);
                idResto = rate.Restaurant_Id;
                if (rate != null)
                {
                    DB.DeleteRating(id);
                }
            }
            return RedirectToAction("Details/", "Restaurants", new { id = ((Restaurant)Session["CurrentRestaurant"]).Id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Rating rate = null;
            Restaurant resto = null;
            using (var DB = new RestaurantsEntities())
            {
                rate = DB.Ratings.Find(id);
                if (rate != null)
                {
                    resto = (Restaurant)Session["CurrentRestaurant"];
                    ViewBag.RestaurantViews = resto.ToRestaurantView();

                    RatingView ratingView = new RatingView();
                    ratingView.Restaurant_Id = ((Restaurant)Session["CurrentRestaurant"]).Id;
                    ratingView = rate.ToRatingView();
                    return View(ratingView);
                }
            }
            return RedirectToAction("Details/" + rate.Restaurant_Id);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ratingView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(RatingView ratingView)
        {
            using (var db = new RestaurantsEntities()) { db.Update(ratingView.ToRating()); }
            return RedirectToAction("Details", "Restaurants", new { id = ((Restaurant)Session["CurrentRestaurant"]).Id });
        }
    }
}