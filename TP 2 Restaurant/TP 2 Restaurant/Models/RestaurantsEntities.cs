using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IRDB.Models
{
    public class RestaurantsEntities : DbContext
    {
        public RestaurantsEntities() : base("name=RestaurantsDBConnectionString") { }
        #region Tables entities
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<PriceRange> PriceRanges { get; set; }
        #endregion

        public System.Data.Entity.DbSet<IRDB.Models.RestaurantView> RestaurantViews { get; set; }

        public System.Data.Entity.DbSet<IRDB.Models.RatingView> RatingViews { get; set; }
    }

    public static class ContactsDBEntities_DAL
    {
        #region Restaurants CRUD functions
        public static Restaurant Create(this RestaurantsEntities DB, Restaurant restaurant)
        {
            Restaurant newRestaurant = DB.Restaurants.Add(restaurant);
            DB.SaveChanges();
            return newRestaurant;
        }
        public static Restaurant Update(this RestaurantsEntities DB, Restaurant restaurant)
        {
            if (restaurant != null)
            {
                Restaurant restaurantToUpdate = DB.Restaurants.Find(restaurant.Id);
                if (restaurantToUpdate != null)
                {
                    restaurantToUpdate.Update(restaurant);

                    DB.Entry(restaurantToUpdate).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return restaurantToUpdate;
                }
            }
            return null;
        }
        private static void UpdateRating(this RestaurantsEntities DB, int restaurantId)
        {
            Restaurant restaurantToUpdate = DB.Restaurants.Find(restaurantId);
            if (restaurantToUpdate != null)
            {
                if ((restaurantToUpdate.Ratings != null) && (restaurantToUpdate.Ratings.Count > 0))
                {
                    double ratingsSum = 0;
                    foreach (Rating rating in restaurantToUpdate.Ratings)
                    {
                        ratingsSum += rating.Rating_Value;
                    }
                    restaurantToUpdate.Rating = ratingsSum / restaurantToUpdate.Ratings.Count;
                }
                else
                {
                    restaurantToUpdate.Rating = 0;
                }
                DB.Entry(restaurantToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }
        public static void DeleteRestaurant(this RestaurantsEntities DB, int restaurantId)
        {
            Restaurant restaurantToDelete = DB.Restaurants.Find(restaurantId);
            if (restaurantToDelete != null)
            {
                // Cascade ratings delete
                DB.Restaurants.Remove(restaurantToDelete);
                DB.SaveChanges();
            }
        }
        public static List<RestaurantView> SortedRestaurantList(this RestaurantsEntities DB, string orderBy, bool ascending)
        {
            List<RestaurantView> items = new List<RestaurantView>();
            List<Restaurant> restaurants = null;
            switch (orderBy)
            {
                case "Name":
                    if (ascending)
                        restaurants = DB.Restaurants.OrderBy(c => c.Name).ToList();
                    else
                        restaurants = DB.Restaurants.OrderByDescending(c => c.Name).ToList();
                    break;
                case "Cuisine":
                    if (ascending)
                        restaurants = DB.Restaurants.OrderBy(c => c.Cuisine.Name).ToList();
                    else
                        restaurants = DB.Restaurants.OrderByDescending(c => c.Cuisine.Name).ToList();
                    break;
                case "Rating":
                    if (ascending)
                        restaurants = DB.Restaurants.OrderBy(c => c.Rating).ToList();
                    else
                        restaurants = DB.Restaurants.OrderByDescending(c => c.Rating).ToList();
                    break;
                case "PriceRange":
                    if (ascending)
                        restaurants = DB.Restaurants.OrderBy(c => c.PriceRange_Id).ToList();
                    else
                        restaurants = DB.Restaurants.OrderByDescending(c => c.PriceRange_Id).ToList();
                    break;
                case "BYOW":
                    if (ascending)
                        restaurants = DB.Restaurants.OrderBy(c => c.BYOW).ToList();
                    else
                        restaurants = DB.Restaurants.OrderByDescending(c => c.BYOW).ToList();
                    break;
            }
            foreach (Restaurant restaurant in restaurants)
            {
                items.Add(restaurant.ToRestaurantView());
            }
            return items;
        }
        #endregion
        #region Ratings CRUD functions
        public static Rating Create(this RestaurantsEntities DB, Rating rating)
        {
            Rating newRating = DB.Ratings.Add(rating);
            DB.UpdateRating(newRating.Restaurant_Id);
            DB.SaveChanges();
            return newRating;
        }
        public static Rating Update(this RestaurantsEntities DB, Rating rating)
        {
            if (rating != null)
            {
                Rating ratingToUpdate = DB.Ratings.Find(rating.Id);
                if (ratingToUpdate != null)
                {
                    ratingToUpdate.Update(rating);
                    DB.Entry(ratingToUpdate).State = System.Data.Entity.EntityState.Modified;
                    DB.UpdateRating(ratingToUpdate.Restaurant_Id);
                    DB.SaveChanges();
                    return ratingToUpdate;
                }
            }
            return null;
        }
        public static void DeleteRating(this RestaurantsEntities DB, int ratingId)
        {
            Rating ratingToDelete = DB.Ratings.Find(ratingId);
            if (ratingToDelete != null)
            {
                int restaurantToUpdateId = ratingToDelete.Restaurant_Id;
                DB.Ratings.Remove(ratingToDelete);
                DB.UpdateRating(restaurantToUpdateId);
                DB.SaveChanges();
            }
        }
        public static List<RatingView> SortedRatingList(this RestaurantsEntities DB, int restaurantId, string orderBy, bool ascending)
        {
            List<RatingView> items = new List<RatingView>();
            Restaurant restaurant = DB.Restaurants.Find(restaurantId);
            if ((restaurant != null) && (restaurant.Ratings != null) && (restaurant.Ratings.Count > 0))
            {

                List<Rating> ratings = null;
                switch (orderBy)
                {
                    case "Name":
                        if (ascending)
                            ratings = restaurant.Ratings.OrderBy(r => r.Rater_Name).ToList();
                        else
                            ratings = restaurant.Ratings.OrderByDescending(r => r.Rater_Name).ToList();
                        break;
                    case "Date":
                        if (ascending)
                            ratings = restaurant.Ratings.OrderBy(r => r.Rating_Date).ToList();
                        else
                            ratings = restaurant.Ratings.OrderByDescending(r => r.Rating_Date).ToList();
                        break;
                    case "Rating":
                        if (ascending)
                            ratings = restaurant.Ratings.OrderBy(r => r.Rating_Value).ToList();
                        else
                            ratings = restaurant.Ratings.OrderByDescending(r => r.Rating_Value).ToList();
                        break;
                 }
                foreach (Rating rating in ratings)
                {
                    items.Add(rating.ToRatingView());
                }      
            }
            return items;
        }
        #endregion
    }
}