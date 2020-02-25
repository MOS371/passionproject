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
     Recipe is added in the cookbook by chef. Each recipe will have only Chef  but one chef can create may recipes in the cookbook
     Also each recipe can have many ingredients and one ingredient can be in nultiple(many) recipes

         recipe can be described with Name, Category, Cusine and Description
         */
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        
        public string Category { get; set; }
        public string Cusine { get; set; }
        //Desription will have the actual steps of the reciepe
        public string Description { get; set; }

        //Representing one to many relationship (one chef to many recipes)
        public int ChefID { get; set; }
        [ForeignKey("ChefID")]
        public virtual Chef Chefs { get; set; }

        //Represnting many to many relationship between recipes and ingredients
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}