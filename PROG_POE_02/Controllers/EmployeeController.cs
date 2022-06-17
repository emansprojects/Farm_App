using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROG_POE_02.Models;

namespace PROG_POE_02.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //GET 
        public ActionResult AddFarmer()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult AddFarmer(string username, string password)
        {
            //EMPLOYEE ADDING A FARMER 

            try
            {
                //CONNECTING TO DB
                farmcentralEntities db = new farmcentralEntities();

                //CONECT TO DB TABLE
                Flogin user = new Flogin();

                //var keyNew = Helper.GeneratePassword(10);
                //password = Helper.EncodePassword(user.password, keyNew);
                //user.password = password;

                //ASSIGNING ITEMS WITH DATA
                user.username = username;
                user.password = password;
               

                //ADDING TO DB TABLE
                db.Flogins.Add(user);
                db.SaveChanges();

                //IF SUCCESSFUL
                ViewBag.Message = string.Format("user added!");
                return RedirectToAction("Welcome", "Home");
            }
            catch (Exception e)
            {
                //IF UNSUCCESSFUL
                ViewBag.Message = string.Format("Unsuccessful entry! Please try again!");
                return RedirectToAction("Contact", "home");
            }
        }

        //GET
        public ActionResult ViewFarmers()
        {
            //VIEW A LIST OF FARMERS IN DB 

            //CONNECTING TO DB 
            farmcentralEntities db = new farmcentralEntities();

            //QUERY TO GET A LIST OF FARMERS FROM FARMERS TABLE 
            var prods = from p in db.Flogins select p;

            //RETURN THE LIST OF FARMERS IN A LIST
            return View(prods);
        }

        //GET
        public ActionResult ViewFarmerProducts(string searching)
        {
            //GETTING PRODUCTS ASSOCIATED WITH EACH FARMER USINNNG SESSION/PATH PARAMETER

            //GETTING THE ID
            string id = Url.RequestContext.RouteData.Values["id"] as string;
            string pathParamID = id;

            //CONNECTING TO DB
            farmcentralEntities db = new farmcentralEntities();

            //QUERY TO DISPLAY ALL PRODUCTS ASSOCATED WITH SELECTED FARMER USING PATH PARAMETER ID
            List<Product> farmerProducts =  db.Products.Where(x => x.username.Equals(pathParamID)).ToList();
          
            //SEARCH A PRODUCT USING PRODUCT TYPE IN THE SEARCH BAR (FILTER)
            if (!String.IsNullOrEmpty(searching))
            {
                var prods = from p in db.Products select p;
                prods = prods.Where(s => s.Product_Type.Contains(searching));
                return View(prods.ToList());
            }

            //ELSE RETURN LIST OF SELECTED FARMERS PRODUCTS
            return View(farmerProducts);
        }
    }
}