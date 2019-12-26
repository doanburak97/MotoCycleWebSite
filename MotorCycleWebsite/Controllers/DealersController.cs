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
    public class DealersController : Controller
    {
        private MotorCycleDBEntities db = new MotorCycleDBEntities();

        // GET: Dealers
        [Route("Dealer/Index")]
        public ActionResult Index()
        {
            return View(db.Dealers.ToList());
        }

        [Route("Dealer/Create")]
        // GET: Dealers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dealers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Dealer/Create")]
        public ActionResult Create([Bind(Include = "DealerId,DealerName,DealerPhone,DealerAddress")] Dealers dealers)
        {
            if (ModelState.IsValid)
            {
                db.Dealers.Add(dealers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dealers);
        }

        // GET: Dealers/Edit/5
        [Route("Dealer/Edit/id")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealers dealers = db.Dealers.Find(id);
            if (dealers == null)
            {
                return HttpNotFound();
            }
            return View(dealers);
        }

        // POST: Dealers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealerId,DealerName,DealerPhone,DealerAddress")] Dealers dealers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dealers);
        }

        // GET: Dealers/Delete/5
        [Route("Dealer/Delete/id")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealers dealers = db.Dealers.Find(id);
            if (dealers == null)
            {
                return HttpNotFound();
            }
            return View(dealers);
        }

        // POST: Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dealers dealers = db.Dealers.Find(id);
            db.Dealers.Remove(dealers);
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
