using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CookBook.Data;
using CookBook.Models;
using System.Diagnostics;
using CookBook.Models.ViewModels;

namespace CookBook.Controllers
{
    public class RecipeController : Controller
    {
        private CookBookContext db = new CookBookContext();
        public ActionResult ListRecipe()
        {
            //How could we modify this to include a search bar?
            List<Recipe> recipes = db.Recipes.SqlQuery("Select * from Recipes").ToList();
            return View(recipes);

        }
        [HttpPost]
        public ActionResult AddRecipe(string RecipeName, string RecipeCategory, String RecipeCusine, int ChefID, string RecipeDescription)
        {
            //STEP 1: PULL DATA! The data is access as arguments to the method. Make sure the datatype is correct!
            //The variable name  MUST match the name attribute described in Views/Pet/Add.cshtml

            //Tests are very useul to determining if you are pulling data correctly!
            //Debug.WriteLine("Want to create a pet with name " + PetName + " and weight " + PetWeight.ToString()) ;

            //STEP 2: FORMAT QUERY! the query will look something like "insert into () values ()"...
            string query = "insert into recipes (RecipeName, Category, Cusine, ChefID, Description) values (@RecipeName,@RecipeCategory,@RecipeCusine,@ChefID,@RecipeDescription)";
            SqlParameter[] sqlparams = new SqlParameter[5]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@RecipeName", RecipeName);
            sqlparams[1] = new SqlParameter("@RecipeCategory", RecipeCategory);
            sqlparams[2] = new SqlParameter("@RecipeCusine", RecipeCusine);
            sqlparams[3] = new SqlParameter("@ChefID", ChefID);
            sqlparams[4] = new SqlParameter("@RecipeDescription", RecipeDescription);

            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Pets.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of pets so we can see our new one!
            return RedirectToAction("ListRecipe");
        }


        public ActionResult AddRecipe()
        {
            //STEP 1: PUSH DATA!
            //What data does the Add.cshtml page need to display the interface?
            //A list of species to choose for a pet

            //alternative way of writing SQL -- will learn more about this week 4
            //List<Species> Species = db.Species.ToList();

            List<Chef> chefs = db.Chefs.SqlQuery("select * from Chefs").ToList();

            return View(chefs);
        }

        public ActionResult UpdateRecipe(int id)
        {
            //need information about a particular pet
            Recipe selectedrecipe = db.Recipes.SqlQuery("select * from recipes where RecipeID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Chef> chefs = db.Chefs.SqlQuery("select * from chefs").ToList();

            UpdateRecipeViewModel UpdateRecipeViewModel = new UpdateRecipeViewModel();
            UpdateRecipeViewModel.Recipe = selectedrecipe;
            UpdateRecipeViewModel.Chefs = chefs;

            return View(UpdateRecipeViewModel);
        }
        [HttpPost]
        public ActionResult UpdateRecipe(int id, string RecipeName, string Category, string Cusine, string Description, int ChefID) { 
       

            //Debug.WriteLine("I am trying to edit a pet's name to "+PetName+" and change the weight to "+PetWeight.ToString());

            string query = "update recipes set RecipeName=@RecipeName, ChefID=@ChefID, Category=@Category, Cusine=@Cusine, Description=@Description where RecipeID=@id";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@RecipeName", RecipeName);
            sqlparams[1] = new SqlParameter("@Category", Category);
            sqlparams[2] = new SqlParameter("@Cusine", Cusine);
            sqlparams[3] = new SqlParameter("@ChefID", ChefID);
            sqlparams[4] = new SqlParameter("@Description", Description);
            sqlparams[5] = new SqlParameter("@id", id);
           

            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the pet in the database goes here
            return RedirectToAction("ListRecipe");
        }


    }
}