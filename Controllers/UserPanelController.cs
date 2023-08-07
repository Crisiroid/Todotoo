using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using todotoo.Data;
using todotoo.Models;
using System.Threading.Tasks;

namespace todotoo.Controllers
{
    public class UserPanelController : Controller
    {
        public int userID;
        private todotooContext db = new todotooContext();
        public ActionResult Index(User user)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (TempData["pm"] != null)
            {
                ViewBag.pm = TempData["pm"].ToString();
            }
            var UserWithTask = db.Users.Include(u => u.tasks).FirstOrDefault(u => u.UserID == user.UserID);
            return View(UserWithTask);
        }
        public ActionResult CreateTask(int id)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            TempData["id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateTask(Tasks task)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            var id = (int)TempData["id"];
            var fUser = db.Users.FirstOrDefault(u => u.UserID == id);
            if(fUser != null)
            {
                task.UserID = id;
                db.Tasks.Add(task);
                db.SaveChanges();
                TempData["pm"] = "Operation Completed!";
                return RedirectToAction("Index", "UserPanel", new { user = fUser });
            }
            else
            {
                TempData["pm"] = "Operation Failed";
                return RedirectToAction("Index", "Userpanel", new { user = fUser });
            }
            
        }

        public ActionResult ChangeStatus(int id)
        {
            Tasks c = (from x in db.Tasks where x.Id == id select x).First();
            TempData["id"] = id;
            return View(c);
        }

        [HttpPost]
        public ActionResult ChangeStatus(Tasks task)
        {
            int id = (int)TempData["id"];
            Tasks c = (from x in db.Tasks where x.Id == id select x).First();
            c.Status = task.Status;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Logout(int id)
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
        }
    }

}