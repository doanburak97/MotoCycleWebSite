using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MotorCycleWebsite.Models;

namespace MotorCycleWebsite.Controllers
{
    public class UsersController : Controller
    {
        private MotorCycleDBEntities db = new MotorCycleDBEntities();

        // GET: Users
        [Route("Users/Index")]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Dealers);
            return View(users.ToList());
        }

        // GET: Users/Create
        [Route("Users/Create")]
        public ActionResult Create()
        {
            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Create")]
        public ActionResult Create([Bind(Include = "UserId,Name,Surname,Mail,Password,UserType,DealerId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName", users.DealerId);
            return View(users);
        }

        // GET: Users/Edit/5
        [Route("Users/Edit/id")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName", users.DealerId);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Surname,Mail,Password,UserType,DealerId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName", users.DealerId);
            return View(users);
        }

        // GET: Users/Delete/5
        [Route("Users/Delete/id")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
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
