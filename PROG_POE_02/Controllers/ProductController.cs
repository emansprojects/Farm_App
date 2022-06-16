using PROG_POE_02.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        public ActionResult AddProduct(string product_name, string product_description, string product_type, string username)
        {
            //adding to Products table 
            Product prod = new Product();

            prod.Product_name = product_name;
            prod.Product_description = product_description;
            prod.Product_Type = product_type;
            prod.username = username;
           
            //adding to db
            farmcentralEntities db = new farmcentralEntities();
            db.Products.Add(prod);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewProducts(string farmerID, string searching)
        {
            farmcentralEntities db = new farmcentralEntities();


            //displaying data where username equals current user

            //return View(db.Products.Where(m => m.id.Equals(farmerID)).ToList());


            //return View(db.Products.Where(m => m.username.Equals(Account.Name)).ToList());

            HttpCookie cookieObj = Request.Cookies["user_id"];
            string _websiteValue = cookieObj["id"];

            List<Product> data = db.Products.Where(x => x.username.Equals(_websiteValue)).ToList();

          

            if (!String.IsNullOrEmpty(searching))
            {
                var prods = from p in db.Products select p;
                prods = prods.Where(s => s.Product_name.Contains(searching));
                return View(prods.ToList());
            }
           
            return View(data);

        }
        //query parameter
    }
}