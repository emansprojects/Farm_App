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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
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
            return View();
        }

        public ActionResult example()
        {
            return View();
        }
    }
}