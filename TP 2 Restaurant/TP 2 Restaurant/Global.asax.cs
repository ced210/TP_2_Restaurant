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
            //    DB.Cuisines.Add(new Cuisine { Name = "Bidon" });
            //    DB.SaveChanges();
            //}

            using (var DB = new IRDB.Models.RestaurantsEntities())
            {
                Restaurant r = new Restaurant();


                r.Id = 0;

                r.Name = "alloresto";

                r.Address = "43 boul duTest";
                r.ZipCode = "J6Z 4H1";
                r.Phone = "450-450-1111";

                r.Website = "http://test.tv";
                r.Cuisine_Id = 1;
                r.PriceRange_Id = 1;
                r.BYOW = true;
                r.Rating = 20.00;
                r.Logo_Id = "0";

                DB.Restaurants.Add(r);
                DB.SaveChanges();
            }
        }
    }
}
