using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Models
{
    /*
     Ingredient can be part of one recipe or many recipes
     Ingredient can be described with its name, category and some notes related to the ingredient
         */
    public class Ingredient
    {
        [Key]
        public int IngredientID { set; get; }
        public string IngredientName { get; set; }

        public string IngredientCategory { get; set; }
        public string IngredientNotes { get; set; }

        //Representing many ingredients in many recipes (many to many relation between ingredients and recipes)
        public ICollection<Recipe> Recipes { get; set; }
    }
}