namespace TP_2_Restaurant.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IRDB.Models.RestaurantsEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "IRDB.Models.RestaurantsEntities";
        }

        protected override void Seed(IRDB.Models.RestaurantsEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
