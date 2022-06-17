using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;

using System.Web.Mvc;
using PROG_POE_02.Models;

namespace PROG_POE_02.Controllers
{
    public class AccountController : Controller
    {
        //FARMER ACCOUNT
        
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //GET FARMER LOGIN
        public ActionResult Verify()
        {
            return View();
        }

        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for(int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }

        //POST FARMER LOGIN
        [HttpPost]
        public ActionResult Verify(string username, string password)
        {
            //FARMER LOGIN 

            //CONNECTING TO DB
            farmcentralEntities db = new farmcentralEntities();
            Flogin fl = new Flogin();

            //HASHING PASSWORD 
            string hashpassword = password;
         
            //FARMER DETAILS TABLE QUERY USING LINQ
            Flogin user = db.Flogins.FirstOrDefault(x => x.username.Equals(username));

            

            //IF FARMER DOES OT EXIST
            if (user == null)
            {

            }
            else
            {
                //IF PASSWORD IS CORRECT ALLOW FARMER TO ENTER
                if (user.password.Equals(hashpassword))
                {
                    //LOGGED IN
                    HttpCookie cookie = new HttpCookie("user_id");
                    cookie["id"] = user.username;

                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie);
                    ViewBag.Message = string.Format("successful");

                    return RedirectToAction("FarmerWelcome", "Home");
                }
                else
                {
                    //PASSWORD ICORRECT
                    ViewBag.Message = string.Format("incorrect");
                    return RedirectToAction("Error", "Shared");
                }
            }

            //IF NONE WORK
            return RedirectToAction("example", "Home");
        }

        //EMPLOYEE ACCOUNT 

        //GET EMPLOYEE LOGIN
        public ActionResult EmployeeVerify()
        {
            return View();
        }

        //POST EMPLOYEE LOGIN
        [HttpPost]
        public ActionResult EmployeeVerify(string username, string password)
        {
            //EMPLOYEE LOG IN 

            //CONECTING TO DB
            farmcentralEntities db = new farmcentralEntities();
            //EMPLOYEE DETAILS TABLE QUERY USING LINQ
            Elogin user = db.Elogins.FirstOrDefault(x => x.username.Equals(username));

            bool verified = Crypto.VerifyHashedPassword(user.password, password);

            //IF EMPLOYEE DOES NOT EXIST
            if (user == null)
            {

            }
            else
            {
                //IF PASSWORD IS CORRECT ALLOW FARMER TO ENTER
                if (verified)
                {
                    //LOGGED IN
                    ViewBag.Message = string.Format("successful");
                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    //PASSWORD INCORRECT
                    ViewBag.Message = string.Format("incorrect");
                    return RedirectToAction("Error", "Shared");
                }
            }
            //IF ONE WORK
            return RedirectToAction("example", "Home");
        }

        //GET EMPLOYEE REGISTER
        public ActionResult EmployeeRegister()
        {
            return View();
        }

        //POST EMPLOYEE REGISTER
        [HttpPost]
        public ActionResult EmployeeRegister(string username, string password)
        {
            try
            {
                //CONECT TO DB
                farmcentralEntities db = new farmcentralEntities();

                

                //var keyNew = Helper.GeneratePassword(10);
                //password = Helper.EncodePassword(user.password, keyNew);
                //user.password = password;

                

                using (db = new farmcentralEntities())
                {
                    //CONNECT TO TABLE IN DB
                    Elogin user = new Elogin();
                    //ASSIGNING VALUES TO TABLE ITEMS
                    user.username = username;
                    //user.password = password;
                    var hash = Crypto.HashPassword(password);
                    user.password = hash;
                    //ADDING ITEMMS TO DB TABLE 
                    db.Elogins.Add(user);
                    db.SaveChanges();
                }
                    

                //IF THE ITEMS ARE ADDED SUCCESSFULLY
                ViewBag.Message = string.Format("user added!");
                return RedirectToAction("EmployeeVerify", "Account");
            }
            catch (Exception e)
            {
                return Content(e.Message);
                //IF ITEMS ARE NOT ADDED
                ViewBag.Message = string.Format("Unsuccessful entry! Please try again!");
                return RedirectToAction("Error", "Shared");
            }
        }
    }
}