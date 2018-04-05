using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IRDB.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Restaurant_Id { get; set; }
        public DateTime Rating_Date { get; set; }
        public string Rater_Name { get; set; }
        public double Rating_Value { get; set; }
        public string Comments { get; set; }

        [ForeignKey("Restaurant_Id")]
        public virtual Restaurant Restaurant { get; set; }
        
        public Rating()
        {
            Restaurant_Id = 0;
            Rating_Date = DateTime.Now;
            Rater_Name = "";
            Rating_Value = 0;
            Comments = "";
        }

        public RatingView ToRatingView()
        {
            RatingView ratingView = new RatingView();
            ratingView.Id = Id;
            ratingView.Restaurant_Id = Restaurant_Id;
            ratingView.Rating_Date = Rating_Date;
            ratingView.Rater_Name = Rater_Name;
            ratingView.Rating_Value = Rating_Value;
            ratingView.Comments = Comments;
            return ratingView;
        }

        public void Update(Rating rating)
        {
            Restaurant_Id = rating.Restaurant_Id;
            Rating_Date = rating.Rating_Date;
            Rater_Name = rating.Rater_Name;
            Rating_Value = rating.Rating_Value;
            Comments = rating.Comments;
        }
    }

    public class RatingView
    {
        public int Id { get; set; }
        public int Restaurant_Id { get; set; }

        [Display(Name = "Date")]
        public DateTime Rating_Date { get; set; }

        [Display(Name = "Nom")]
        [StringLength(50), Required(ErrorMessage = "Nom obligatoire.")]
        [RegularExpression(@"^((?!^Name$)[-a-zA-Z0-9àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_. '])+$", ErrorMessage = "Caractères illégaux.")]
        public string Rater_Name { get; set; }

        [Display(Name = "Evaluation")]
        [Range(1, 5, ErrorMessage = "Veuillez faire une évaluation.")]
        public double Rating_Value { get; set; }

        [Display(Name = "Commentaires")]
        public string Comments { get; set; }

        public RatingView()
        {
            Restaurant_Id = 0;
            Rating_Date = DateTime.Now;
            Rater_Name = "";
            Rating_Value = 0;
            Comments = "";
        }
        public Rating ToRating()
        {
            Rating rating = new Rating();
            rating.Id = Id;
            rating.Restaurant_Id = Restaurant_Id;
            rating.Rating_Date = Rating_Date;
            rating.Rater_Name = Rater_Name;
            rating.Rating_Value = Rating_Value;
            rating.Comments = Comments;
            return rating;
        }
    }
}