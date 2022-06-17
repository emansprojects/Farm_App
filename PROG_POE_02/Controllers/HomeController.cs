using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROG_POE_02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ABOUT FARM CENTRAL 
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //CONTACT DETAILS
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Employee()
        {
            return View("Verify","Account");
        }

        public ActionResult Farmer()
        {
            return View();
        }

        public ActionResult SelectionScreen()
        {
            //A USER CAN SELECT IF THEY ARE A FARMER OR AN EMPLOYEE 
            return View();
        }

        public ActionResult Welcome()
        {
            //WELCOME PAGE FOR EMPLOYEE
            return View();
        }

        public ActionResult FarmerWelcome()
        {
            //WELCOME PAGE FOR FARMER
            return View();
        }

        public ActionResult example()
        {
            //WEBSITE ETRANCE 
            return View();
        }
    }
}