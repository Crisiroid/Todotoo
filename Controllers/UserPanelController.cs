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
            
            return View(user);
        }
        public ActionResult ManageAssignment(int id)
        {
            var UserWithTask = db.Users.Include(u => u.tasks).FirstOrDefault(u => u.UserID == id);
            return View(UserWithTask);
        }
        public ActionResult CreateTask(int id)
        {
            userID = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateTask(Tasks task)
        {
            //task.UserID = userID;
            var user = db.Users.FirstOrDefault(u => u.UserID == userID);
            task.UserID = user.UserID;
            db.Tasks.Add(task);
            db.SaveChanges();
            return RedirectToAction("ManageAssignment", "UserPanel");
        }
    }

}