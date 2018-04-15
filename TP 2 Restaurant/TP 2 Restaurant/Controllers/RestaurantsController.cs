﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IRDB.Models;

namespace TP_2_Restaurant.Controllers
{
    public class RestaurantsController : Controller
    {
        ///<permission cref="">Ced</permission>
        /// <summary>
        /// Ced 8 Avril
        /// 
        /// Initialise le tri à Par Nom
        /// et en ordre croissant
        /// </summary>
        private void InitializeSessionSort()
        {
            if (Session["RestaurantSortBy"] ==  null)
            {
                Session["RestaurantSortBy"] = "Name";
                Session["RestaurantSortAscendant"] = true;
            }
        }

        ///<permission cref="">Ced</permission>
        /// <summary>
        /// Ced 8 Avril
        /// 
        /// Tri selon l'attribut
        /// </summary>
        public ActionResult Sort(string by)
        {
            if (by == (string)Session["RestaurantSortBy"])
                // Inverse le tri
                Session["RestaurantSortAscendant"] = !(bool)Session["RestaurantSortAscendant"];
            else
                Session["RestaurantSortAscendant"] = true;
            //Donne le tri demander
            Session["RestaurantSortBy"] = by;
            //Redirige vers l'action Index()
            return RedirectToAction("Index");

        }
        /// <summary>
        /// Ced 15 Av
        /// 
        /// Initialise le tri par Date
        /// et en ordre décroissant
        /// </summary>
        private void InitializeRatingSessionSort()
        {
            if (Session["RatingSortBy"] == null)
            {
                //Par défault,
                // Tri par date
                Session["RatingSortBy"] = "Date";
                // Du plus Récent
                Session["RatingSortAscendant"] = false;
            }
        }
        /// <summary>
        /// Ced 15 av
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public ActionResult SortRating(string by)
        {
            if (by == (string)Session["RatingSortBy"])
                Session["RatingSortAscendant"] = !(bool)Session["RatingSortAscendant"];
            else
                Session["RatingSortAscendant"] = true;
            Session["RatingSortBy"] = by;
            //return View("allo");
            Restaurant r = (Restaurant)Session["CurrentRestaurant"];
            return RedirectToAction("Details/" + r.Id);
        }
        // GET: Restaurants
        public ActionResult Index()
        {
            //List<RestaurantView> db = new List<RestaurantView>();
            //db.Add(new Restaurant { Name = "Test" }.ToRestaurantView());

            InitializeSessionSort();
            using (var DB = new RestaurantsEntities())
            {
                return View(DB.SortedRestaurantList((string)Session["RestaurantSortBy"], (bool)Session["RestaurantSortAscendant"])); 
            }
        }

        /// <summary>
        /// Ced 12 avril MOD
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Details(int Id)
        {
            Restaurant resto = null; 
            using (var DB = new RestaurantsEntities())
            {
                resto = DB.Restaurants.Find(Id);
                if (resto != null)
                {
                    InitializeRatingSessionSort();
                    Session["CurrentRestaurant"] = resto;
                    ViewBag.RestaurantViews = resto.ToRestaurantView();
                    //ViewBag.Ratings = DB.Ratings.Where(r => r.Restaurant_Id == resto.Id);
                    return View(DB.SortedRatingList(Id,(string)Session["RatingSortBy"],(bool)Session["RatingSortAscendant"]));
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Dom 12 av
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            using (var DB = new RestaurantsEntities())
            {
                RestaurantView restoView = new RestaurantView();
                ViewBag.Cuisines = DB.Cuisines.ToList();
                ViewBag.PriceRanges = DB.PriceRanges.ToList();
                return View(restoView);
            }
        }
        /// <summary>
        /// Dom 12 av
        /// </summary>
        /// <param name="restaurantView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(RestaurantView restaurantView)
        {
            restaurantView.UpLoadLogo(Request);
            using (var DB = new RestaurantsEntities())
            {
                ViewBag.Cuisines = DB.Cuisines.ToList();
                ViewBag.PriceRanges = DB.PriceRanges.ToList();
                if (ModelState.IsValid)
                {
                    Restaurant resto = new Restaurant();
                    resto = restaurantView.ToRestaurant();

                    DB.Restaurants.Add(resto);
                    DB.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(restaurantView);
            }
        }

        /// <summary>
        /// Dom 12 av
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            using (var DB = new RestaurantsEntities())
            {
                Restaurant resto = DB.Restaurants.Find(id);
                if (resto != null)
                {
                    resto.ToRestaurantView().RemoveLogo();
                    DB.DeleteRestaurant(id);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Dom 12 av
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Restaurant resto = null;
            using (var DB = new RestaurantsEntities())
            {
                resto = DB.Restaurants.Find(id);
                if (resto != null)
                {
                    RestaurantView restoView = new RestaurantView();
                    ViewBag.Cuisines = DB.Cuisines.ToList();
                    ViewBag.PriceRanges = DB.PriceRanges.ToList();
                    restoView = resto.ToRestaurantView();
                    return View(restoView);
                }
            }
            return RedirectToAction("Index");

        }

        /// <summary>
        /// Dom 12 av
        /// </summary>
        /// <param name="restaurantView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(RestaurantView restaurantView)
        {
            restaurantView.UpLoadLogo(Request);
            using (var db = new RestaurantsEntities()) { db.Update(restaurantView.ToRestaurant()); }
            return RedirectToAction("Index");
        }
    }
}