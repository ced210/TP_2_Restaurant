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

            ///Ajouter un resto au demarage
            /*using (var DB = new IRDB.Models.RestaurantsEntities())
            {
                Restaurant r = new Restaurant();
                r.Id = 1;
                r.Name = "alloresto2";
                r.Address = "43 boul duTest2";
                r.ZipCode = "J2Z 4H1";
                r.Phone = "514-450-1111";
                r.Website = "http://test222.tv";
                r.Cuisine_Id = 2;
                r.PriceRange_Id = 2;
                r.BYOW = false;
                r.Rating = 2.00;
                r.Logo_Id = "2";

                DB.Restaurants.Add(r);
                DB.SaveChanges();
            }*/

            /*using (var DB = new IRDB.Models.RestaurantsEntities())
            {
                Restaurant r = new Restaurant();
                r.Id = 1;
                r.Name = "alloresto2";
                r.Address = "43 boul duTest2";
                r.ZipCode = "J2Z 4H1";
                r.Phone = "514-450-1111";
                r.Website = "http://test222.tv";
                r.Cuisine_Id = 2;
                r.PriceRange_Id = 2;
                r.BYOW = false;
                r.Rating = 2.00;
                r.Logo_Id = "5";

                DB.Restaurants.Add(r);
                DB.SaveChanges();
            }*/


            /*using (var DB = new IRDB.Models.RestaurantsEntities())
            {
                Rating r = new Rating();
                r.Id = 1;
                r.Comments = "alllooooo comment";

                r.Rating_Date = DateTime.Now;
                r.Rating_Value = 3;
                r.Restaurant_Id = 1;

                DB.Ratings.Add(r);
                DB.SaveChanges();
            }*/
        }
    }
}
