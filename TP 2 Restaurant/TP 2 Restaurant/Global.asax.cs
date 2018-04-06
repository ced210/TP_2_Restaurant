using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IRDB.Models;

namespace TP_2_Restaurant
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //obliger a créer unne Table Cuisine dans la BD,
            // ensuite, on ajoute les éléments manuelement à la BD
            //using (var DB = new IRDB.Models.RestaurantsEntities())
            //{
            //    //    DB.Cuisines.Add(new Cuisine { Name = "Bidon" });

            //    Restaurant resto = new Restaurant
            //    {
            //        Id = 0,
            //        Name = "Test",
            //        Address = "123 test",
            //        ZipCode = "G4S3H5".ToUpper(),
            //        Phone = "(542)435-2345",
            //        Website = "https://test.com",
            //        Cuisine_Id = 3,
            //        PriceRange_Id = 2,
            //        BYOW = false,
            //        Rating = 4,
            //        Logo_Id = "./RestaurantLogos/restaurant-icons.png"
            //    };

            //    DB.Restaurants.Add(resto);
            //    DB.SaveChanges();
            //}
        }
    }
}
