using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROG_POE_02.Models;

namespace PROG_POE_02.Controllers
{
    public class AccountController : Controller
    {
        
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Verify()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Verify(string username, string password)
        {
            string passwordHash = password;

            Account.Name = username;
         

            //user login 
            farmcentralEntities db = new farmcentralEntities();
            Flogin user = db.Flogins.FirstOrDefault(x => x.username.Equals(username));
            

            //if no user then show this
            if (user == null)
            {

            }
            else
            {
                //if password equals to password that user entered then allow them to enter
                if (user.password.Equals(passwordHash))
                {
                    //logged in
                    HttpCookie cookie = new HttpCookie("user_id");
                    cookie["id"] = user.username;
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie);
                    ViewBag.Message = string.Format("successful");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //password incorect
                    ViewBag.Message = string.Format("incorrect");
                    return RedirectToAction("Contact", "Home");
                }
            }
            return RedirectToAction("About", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
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


        //employee Account 

        public ActionResult EmployeeVerify()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeVerify(string username, string password)
        {
            string passwordHash = password;

            Account.Name = username;


            //user login 
            farmcentralEntities db = new farmcentralEntities();
            Elogin user = db.Elogins.FirstOrDefault(x => x.username.Equals(username));


            //if no user then show this
            if (user == null)
            {

            }
            else
            {
                //if password equals to password that user entered then allow them to enter
                if (user.password.Equals(passwordHash))
                {
                    //logged in
                    ViewBag.Message = string.Format("successful");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //password incorect
                    ViewBag.Message = string.Format("incorrect");
                    return RedirectToAction("Contact", "Home");
                }
            }
            return RedirectToAction("About", "Home");
        }

        public ActionResult EmployeeRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeRegister(string username, string password)
        {
            try
            {
                //entering into db tables
                farmcentralEntities db = new farmcentralEntities();
                Elogin user = new Elogin();

                //var keyNew = Helper.GeneratePassword(10);
                //password = Helper.EncodePassword(user.password, keyNew);
                //user.password = password;

                user.username = username;
                user.password = password;

                db.Elogins.Add(user);
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
    }
}