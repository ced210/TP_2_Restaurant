using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IRDB.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public int Cuisine_Id { get; set; }
        public int PriceRange_Id { get; set; }
        public bool BYOW { get; set; }
        public double Rating { get; set; }
        public string Logo_Id { get; set; }

	//////////Membre de navigation EF
        public virtual List<Rating> Ratings { get; set; }
		//FK pour l'intégrité relationel en la table et l'Id
        [ForeignKey("Cuisine_Id")]
        public virtual Cuisine Cuisine { get; set; }
        [ForeignKey("PriceRange_Id")]
        public virtual PriceRange PriceRange { get; set; }


        public RestaurantView ToRestaurantView()
        {
            RestaurantView restaurantView = new RestaurantView();
            restaurantView.Id = Id;
            restaurantView.Name = Name;
            restaurantView.Address = Address;
            restaurantView.ZipCode = ZipCode.ToUpper();
            restaurantView.Phone = Phone;
            restaurantView.Website = Website;
            restaurantView.Cuisine_Id = Cuisine_Id;
            restaurantView.PriceRange_Id = PriceRange_Id;
            restaurantView.BYOW = BYOW;
            restaurantView.Rating = Rating;
            restaurantView.Logo_Id = Logo_Id;
            if (Cuisine != null)
                restaurantView.Cuisine = Cuisine.Name;
            if (PriceRange != null)
                restaurantView.PriceRange = PriceRange.Name;
            if (Ratings != null)
            {
                restaurantView.NbRatings = Ratings.Count;
            }
            return restaurantView;
        }

        public void Update(Restaurant restaurant)
        {
            Name = restaurant.Name;
            Address = restaurant.Address;
            ZipCode = restaurant.ZipCode;
            Phone = restaurant.Phone;
            Website = restaurant.Website;
            Cuisine_Id = restaurant.Cuisine_Id;
            PriceRange_Id = restaurant.PriceRange_Id;
            BYOW = restaurant.BYOW;
            Rating = restaurant.Rating;
            Logo_Id = restaurant.Logo_Id;
        }
    }

    public class RestaurantView
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        [StringLength(50), Required(ErrorMessage = "Nom obligatoire.")]
        [RegularExpression(@"^((?!^Name$)[-a-zA-Z0-9àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_. '])+$", ErrorMessage = "Caractères illégaux.")]
        public string Name { get; set; }

        [Display(Name = "Adresse")]
        [StringLength(150), Required(ErrorMessage = "Adresse obligatoire.")]
        public string Address { get; set; }

        [Display(Name = "Code postal")]
        [StringLength(8), Required(ErrorMessage = "Code postal obligatoire.")]
        public string ZipCode { get; set; }

        [Display(Name = "Téléphone")]
        [StringLength(14), Required(ErrorMessage = "Téléphone obligatoire.")]
        public string Phone { get; set; }

        [Display(Name = "Site Web")]
        [StringLength(255), Required(ErrorMessage = "Site Web obligatoire.")]
        [Url(ErrorMessage = "Entrez un URL valide.")]
        public string Website { get; set; }

        [Display(Name = "Cuisine")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez faire une sélection.")]
        public int Cuisine_Id { get; set; }

        [Display(Name = "Budget")]
        [Range(1, 5, ErrorMessage = "Veuillez faire une sélection.")]
        public int PriceRange_Id { get; set; }

        [Display(Name = "Apporter son vin")]
        public bool BYOW { get; set; }

        [Display(Name = "Evaluations")]
        public double Rating { get; set; }

        [Display(Name = "Logo")]
        public string Logo_Id { get; set; }

        private App_Utilities.ImageGUIDReference LogoReference;
        public RestaurantView()
        {
            Name = "";
            Address = "";
            ZipCode = "";
            Phone = "";
            Website = "";
            Cuisine_Id = 0;
            PriceRange_Id = 0;
            Logo_Id = "";
            Rating = 0;
            LogoReference = new App_Utilities.ImageGUIDReference(@"/RestaurantLogos/", @"restaurant-icon.png");
        }

        public string Cuisine { get; set; }
        public string PriceRange { get; set; }

        public int NbRatings { get; set; }

        public List<PriceRange> PriceRanges
        {
            get
            {
                using (var DB = new RestaurantsEntities())
                {
                    return DB.PriceRanges.ToList();
                }
            }
        }

        public List<Cuisine> Cuisines
        {
            get
            {
                using (var DB = new RestaurantsEntities())
                {
                    return DB.Cuisines.ToList();
                }
            }
        }

        public Restaurant ToRestaurant()
        {
            Restaurant restaurant = new Restaurant();
            restaurant.Id = Id;
            restaurant.Name = Name;
            restaurant.Address = Address;
            restaurant.ZipCode = ZipCode.ToUpper();
            restaurant.Phone = Phone;
            restaurant.Website = Website;
            restaurant.Cuisine_Id = Cuisine_Id;
            restaurant.PriceRange_Id = PriceRange_Id;
            restaurant.BYOW = BYOW;
            restaurant.Rating = Rating;
            restaurant.Logo_Id = Logo_Id;
            return restaurant;
        }
        public String GetLogoURL()
        {
            return LogoReference.GetURL(Logo_Id);
        }
        public void UpLoadLogo(HttpRequestBase Request)
        {
            Logo_Id = LogoReference.UpLoadImage(Request, Logo_Id);
        }
        public void RemoveLogo()
        {
            LogoReference.Remove(Logo_Id);
        }
    }
}