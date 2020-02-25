using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CookBook.Data
{
    public class CookBookContext : DbContext
    {
       
 
        public CookBookContext() : base("name=CookBookContext")
        {
        }

        public System.Data.Entity.DbSet<CookBook.Models.Chef> Chefs { get; set; }

        public System.Data.Entity.DbSet<CookBook.Models.Recipe> Recipes { get; set; }
        public System.Data.Entity.DbSet<CookBook.Models.Ingredient> Ingredients { get; set; }

    }
}
