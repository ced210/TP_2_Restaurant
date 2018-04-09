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
        // GET: Restaurants
        public ActionResult Index()
        {
            List<RestaurantView> db = new List<RestaurantView>();
            //db.Add(new Restaurant { Name = "Test" }.ToRestaurantView());
            return View(db);
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
            using (var DB = new RestaurantsEntities())
            {
                ViewBag.Cuisines = DB.Cuisines.ToList();
                ViewBag.PriceRanges = DB.PriceRanges.ToList();
                if (!ModelState.IsValid)
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
                    restaurantView.UpLoadLogo(Request);
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
                return View();
            }
        }
    }
}