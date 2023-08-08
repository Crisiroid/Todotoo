using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using todotoo.Data;
using todotoo.Models;

namespace todotoo.Controllers
{
    public class UsersController : Controller
    {
        private todotooContext db = new todotooContext();

        public ActionResult Index(User user)
        {
            if (Session["username"].ToString() != "Admin") return RedirectToAction("Index", "Home");
            return View(user);
            
        }
        public ActionResult ViewUsers()
        {
            return View(db.Users.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FullName,Username,Password,Email,LastActiveDate")] User user)
        {
            user.LastActiveDate = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("ViewUsers", "Users");
            }

            return View(user);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FullName,Username,Password,Email,LastActiveDate")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewUsers");
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ViewUsers");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateContent(Content c)
        {
            c.Created = DateTime.Now;
            db.Contents.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
