using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CookBook.Data;
using CookBook.Models;
using System.Diagnostics;

namespace CookBook.Controllers
{
    public class ChefController : Controller
    {
        private CookBookContext db = new CookBookContext();
        //Get Chef
        public ActionResult Index()
        {
            return View();
        }
        //To display the list of Chefs
        public ActionResult ListChef()
        {
            List<Chef> mychefslist = db.Chefs.SqlQuery("Select * from chefs").ToList();

            return View(mychefslist);
        }

      
        
        //To add a new chef to the existing list
        public ActionResult AddChef()
        {
           
            return View();
        }
        [HttpPost]
        //newChefName is the variable that we are taking to send the data into the "chefName" column of the Chefs table through sql injection
        //chefName is the column name in Chefs table
        public ActionResult AddChef(string chefName, int? ChefExperience, string ChefSpeciality)
        {
            Debug.WriteLine(chefName);
            string query = "insert into chefs (chefName, experience, speciality) values (@ChefName, @Experience, @Speciality )";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@ChefName", chefName);
            sqlparams[1] = new SqlParameter("@Experience", ChefExperience);
            sqlparams[2] = new SqlParameter("@Speciality", ChefSpeciality);

            db.Database.ExecuteSqlCommand(query, sqlparams);
         

            return RedirectToAction("ListChef");
        }
        public ActionResult ShowChef(int id)
        {
            string query = "select * from chefs where chefID = @id";
            var parameter = new SqlParameter("@id", id);
            Chef selectedchef = db.Chefs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedchef);
        }
        //Method to Update the existing chef in the DB
        public ActionResult UpdateChef(int id)
        {
            string query = "select * from chefs where chefID = @id";
            var parameter = new SqlParameter("@id", id);
            Chef selectedchef = db.Chefs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedchef);
        }
        [HttpPost]
        //newChefName, newExperience, newSpeciality are the new variables that are sent to the sql query throuh sql injection
        public ActionResult UpdateChef(int id, string ChefName, int ChefExperience, string ChefSpeciality)
        {
            string query = "update chefs set chefName = @ChefName, experience= @Experience, speciality= @Speciality where chefID = @id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@ChefName", ChefName);
            sqlparams[1] = new SqlParameter("@Experience", ChefExperience);
            sqlparams[2] = new SqlParameter("@Speciality", ChefSpeciality);
            sqlparams[3] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("ListChef");
        }
        public ActionResult DeleteChef(int id)
        {
            string query = "select * from chefs where chefID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Chef selectedchef = db.Chefs.SqlQuery(query, param).FirstOrDefault();
            return View(selectedchef);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from chefs where chefID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


            //for the sake of referential integrity, unset the Chef for all the recipes
            string refquery = "update recipes set chefID = '' where chefID=@id";
            db.Database.ExecuteSqlCommand(refquery, param); //same param as before

            return RedirectToAction("ListChef");
        }


    }
}