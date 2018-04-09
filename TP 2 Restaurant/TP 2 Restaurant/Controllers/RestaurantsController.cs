using System;
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
                Session["RestauratSortAscendant"] = !(bool)Session["RestaurantSortAscendant"];
            else
                Session["RestaurantSortAscendant"] = true;
            //Donne le tri demander
            Session["RestaurantSortBy"] = by;
            //Redirige vers l'action Index()
            return RedirectToAction("Index");

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



        public ActionResult Create()
        {
            using (var DB = new RestaurantsEntities())
            {
                ViewBag.Cuisines = DB.Cuisines.ToList();
                return View();
            }
        }

            [HttpPost]
        public ActionResult Create(RestaurantView restaurantView)
        {
            using (var DB = new RestaurantsEntities())
            {
                ViewBag.Cuisines = DB.Cuisines.ToList();
                if (ModelState.IsValid)
                {
                    //if (!string.IsNullOrEmpty(restaurantView.))
                    //{
                    //    if (!DB.CategoryExist(restaurantView.newCategory))
                    //    {
                    //        Category new_Category = DB.Add(new Category { Id = 0, Name = restaurantView.newCategory });
                    //        restaurantView.CategoryId = new_Category.Id;
                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("NewCategory", "This category already exist.");
                    //        return View();
                    //    }
                    //}
                    Restaurant resto = new Restaurant();
                    resto = restaurantView.ToRestaurant();
                    resto.Logo_Id = "./RestaurantLogos/restaurant-icon.png";
                    DB.Restaurants.Add(resto);
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
    }
}