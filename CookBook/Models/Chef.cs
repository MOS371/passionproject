using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CookBook.Models
{
    public class Chef
    {
        [Key]
        public int chefID { get; set; }
        public string chefName { get; set; }
        //experience in years not months
        public int experience { get; set; }
        public string speciality { get; set; }

        //Representing many in the below statement where one chef can write many recipes in the cookbook
        public ICollection<Recipe> Recipes { get; set; }

          
    }
}