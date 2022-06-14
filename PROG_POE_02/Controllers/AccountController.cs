using System;
using System.Collections.Generic;
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
            PROG_TASK2 db = new PROG_TASK2();
            Ulogin user = db.Ulogins.FirstOrDefault(x => x.Username.Equals(username));

            //if no user then show this
            if (user == null)
            {

            }
            else
            {
                //if password equals to password that user entered then allow them to enter
                if (user.Password.Equals(passwordHash))
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            try
            {
                string usernames = username;
              
                //entering into db tables
                PROG_TASK2 db = new PROG_TASK2();
                Ulogin user = new Ulogin();

                var keyNew = Helper.GeneratePassword(10);
                password = Helper.EncodePassword(user.Password, keyNew);
                user.Password = password;

                user.Username = usernames;
                user.Password = password;

                db.Ulogins.Add(user);
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