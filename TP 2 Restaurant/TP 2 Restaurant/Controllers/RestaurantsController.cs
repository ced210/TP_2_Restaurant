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
                RestaurantView restoView = new RestaurantView();
                ViewBag.Cuisines = DB.Cuisines.ToList();
                ViewBag.PriceRanges = DB.PriceRanges.ToList();
                return View(restoView);
            }
        }

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

                    //resto.Id = 1;
                    //resto.Name = "Test 2";
                    //resto.Address = "123 test";
                    //resto.ZipCode = "G4S3H5".ToUpper();
                    //resto.Phone = "(542)435-2345";
                    //resto.Website = "https://test.com";
                    //resto.Cuisine_Id = 3;
                    //resto.PriceRange_Id = 2;
                    //resto.BYOW = false;
                    //resto.Rating = 0;
                    //resto.Logo_Id = "./RestaurantLogos/restaurant-icons.png";

                    DB.Restaurants.Add(resto);
                    DB.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(restaurantView);
            }
        }
    }
}