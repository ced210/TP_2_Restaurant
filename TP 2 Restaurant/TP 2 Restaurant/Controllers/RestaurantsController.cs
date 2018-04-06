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
                    DB.Create(resto);
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
    }
}