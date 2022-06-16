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

        public ActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFarmer(string username, string password)
        {
            try
            {
                //entering into db tables
                farmcentralEntities db = new farmcentralEntities();
                Flogin user = new Flogin();

                //var keyNew = Helper.GeneratePassword(10);
                //password = Helper.EncodePassword(user.password, keyNew);
                //user.password = password;

                user.username = username;
                user.password = password;

                db.Flogins.Add(user);
                db.SaveChanges();

                ViewBag.Message = string.Format("user added!");
                return RedirectToAction("Verify", "Account");
            }
            catch (Exception e)
            {
                ViewBag.Message = string.Format("Unsuccessful entry! Please try again!");
                return RedirectToAction("Register", "Account");
            }
        }


        public ActionResult ViewFarmers()
        {
            farmcentralEntities db = new farmcentralEntities();
            var prods = from p in db.Flogins select p;
            return View(prods);
        }

        public ActionResult ViewFarmerProducts()
        {
            string id = Url.RequestContext.RouteData.Values["id"] as string;
            //path paramter => www.somethig.com/Admin
            //query parameter => www.something.com/abc?date=01012020
            string pathParamID = id;
            farmcentralEntities db = new farmcentralEntities();
            List<Product> farmerProducts =  db.Products.Where(x => x.username.Equals(pathParamID)).ToList();
            return View(farmerProducts);
        }
    }
}