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

        //GET 
        public ActionResult AddProduct()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult AddProduct(string product_name, string product_description, string product_type, string username)
        {
            //ADDING A PRODUCT

            //CONNECTING TO PRODUCTS TABLE 
            Product prod = new Product();

            //ASSIGNING VALUES TO ENTER INTO PRODUCTS TABLE
            prod.Product_name = product_name;
            prod.Product_description = product_description;
            prod.Product_Type = product_type;
            prod.username = username;
           
            //CONNECTING TO DB
            farmcentralEntities db = new farmcentralEntities();

            //ADDING ITEMS TO PRODUCTS TABLE
            db.Products.Add(prod);
            db.SaveChanges();

            //RETURN TO THIS VIEW AFTER CLICKING ADD
            return RedirectToAction("ViewProducts", "Product");
        }

        //GET
        public ActionResult ViewProducts(string farmerID, string searching)
        {
            //DISPLAYING PRODUCTS FOR FARMER

            //COECTING TO DB
            farmcentralEntities db = new farmcentralEntities();

            //USING COOKIES TO GET USER LOGGED IN 
            HttpCookie cookieObj = Request.Cookies["user_id"];
            string _websiteValue = cookieObj["id"];

            //QUERY TO FINND ALL PRODUCTS UNDER THE USERNAME THAT HAS LOGGED IN
            List<Product> data = db.Products.Where(x => x.username.Equals(_websiteValue)).ToList();

            //SEARCH A PRODUCT USING PRODUCT TYPE IN THE SEARCH BAR (FILTER)
            if (!String.IsNullOrEmpty(searching))
            {
                var prods = from p in db.Products select p;
                prods = prods.Where(s => s.Product_Type.Contains(searching));
                return View(prods.ToList());
            }
            //ELSE RETURN LIST OF SELECTED FARMERS PRODUCTS
            return View(data);
        }
    }
}