using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookBook.Models.ViewModels
{
    public class UpdateRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Chef> Chefs { get; set; }
    }
}