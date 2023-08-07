using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todotoo.Data;
using todotoo.Models;

namespace todotoo.Controllers
{
    public class HomeController : Controller
    {
        private todotooContext db = new todotooContext();
        public ActionResult Index()
        {
            if (TempData["pm"]!= null)
            {
                ViewBag.pm = TempData["pm"];
            }
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            if(CheckInformation(Username, Password))
            {
                User user;
                ViewBag.pm = "Login SuccessFull";
                if (Username == "Crisiroid")
                {
                    Session["username"] = "Admin";
                    user = db.Users.FirstOrDefault(u => u.Username == Username);
                    return RedirectToAction("Index", "Users", user);
                }
                else
                {
                    Session["username"] = Username;
                    user = db.Users.FirstOrDefault(u => u.Username == Username);
                    return RedirectToAction("Index", "UserPanel", user);
                }
                
            }
            else {
                TempData["pm"] = "Login Failed!";
                return RedirectToAction("Index", "Home");
            }
            
        }

        public bool CheckInformation(string Username, String Password)
        {
            return db.Users.Any(u => u.Username == Username && u.Password == Password);

        }
    }
}