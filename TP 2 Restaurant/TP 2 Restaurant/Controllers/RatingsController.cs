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
        #region Rating Sort

        ///// <summary>
        ///// Ced 15 Av
        ///// 
        ///// Initialise le tri par Date
        ///// et en ordre décroissant
        ///// </summary>
        //private void InitializeRatingSessionSort()
        //{
        //    if (Session["RatingSortBy"] == null)
        //    {
        //        //Par défault,
        //        // Tri par date
        //        Session["RatingSortBy"] = "Date";
        //        // Du plus Récent
        //        Session["RatingSortAscendant"] = false;
        //    }
        //}

        ///// <summary>
        ///// Ced 15 av
        ///// </summary>
        ///// <param name="by"></param>
        ///// <returns></returns>
        //public ActionResult Sort(string by)
        //{
        //    if (by == (string)Session["RatingSortAscendant"])
        //        Session["RatingSortAscendant"] = !(bool)Session["RatingSortAscendant"];
        //    else
        //        Session["RatingSortAscendant"] = true;
        //    Session["RatingSortBy"] = by;
        //    return View("allo");
        //}
        #endregion





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
            //ced 15 av
            //Pour En-tete du resto courrant
            Restaurant resto = null;

            using (var DB = new RestaurantsEntities())
            {
                //ced 15 av
                //Pour En-tete du resto courrant
                resto = (Restaurant)Session["CurrentRestaurant"];
                ViewBag.RestaurantViews = resto.ToRestaurantView();

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
        /// <summary>
        /// Dom 15 av
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
            //return RedirectToAction("Details/" + idResto);
        }
    }
}