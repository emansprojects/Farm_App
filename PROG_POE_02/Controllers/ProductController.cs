using PROG_POE_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROG_POE_02.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string product_name, string product_description, string product_type)
        {
            //adding to Products table 
            Product prod = new Product();

            prod.Product_name = product_name;
            prod.Product_Description = product_description;
            prod.Product_Type = product_type;
           
            //adding to db
            PROG_TASK2 db = new PROG_TASK2();
            db.Products.Add(prod);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult ViewProducts(string farmerID, string searching)
        {
            //displaying data where username equals current user
            PROG_TASK2 db = new PROG_TASK2();

            //return View(db.Products.Where(m => m.id.Equals(farmerID)).ToList());
            //return View();

            var prods = from p in db.Products select p;

            if(!String.IsNullOrEmpty(searching))
            {
                prods = prods.Where(s => s.Product_name.Contains(searching));
            }
            return View(prods.ToList());

        }
        //query parameter
    }
}