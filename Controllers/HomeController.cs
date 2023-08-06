using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todotoo.Data;

namespace todotoo.Controllers
{
    public class HomeController : Controller
    {
        private todotooContext db = new todotooContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Login(string Username, string Password)
        {
            if(CheckInformation(Username, Password))
            {
                ViewBag.pm = "Login SuccessFull";
                return RedirectToAction("Index", "Home");
            }
            else {
                ViewBag.pm = "Login Failed!";
                return RedirectToAction("Index", "Home"); }
            
        }

        public bool CheckInformation(string Username, String Password)
        {
            return db.Users.Any(u => u.Username == Username && u.Password == Password);

        }
    }
}