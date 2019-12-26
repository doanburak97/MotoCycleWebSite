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
    public class DealerProductsController : Controller
    {
        private MotorCycleDBEntities db = new MotorCycleDBEntities();

        // GET: DealerProducts
        [Route("DealerProducts/Index")]
        public ActionResult Index()
        {
            var dealerProducts = db.DealerProducts.Include(d => d.Dealers).Include(d => d.Products);
            return View(dealerProducts.ToList());
        }

        // GET: DealerProducts/Create
        [Route("DealerProducts/Create")]
        public ActionResult Create()
        {
            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: DealerProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DealerProductId,DealerId,ProductId")] DealerProducts dealerProducts)
        {
            if (ModelState.IsValid)
            {
                db.DealerProducts.Add(dealerProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName", dealerProducts.DealerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", dealerProducts.ProductId);
            return View(dealerProducts);
        }

        // GET: DealerProducts/Edit/5
        [Route("DealerProducts/Edit/id")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DealerProducts dealerProducts = db.DealerProducts.Find(id);
            if (dealerProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName", dealerProducts.DealerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", dealerProducts.ProductId);
            return View(dealerProducts);
        }

        // POST: DealerProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealerProductId,DealerId,ProductId")] DealerProducts dealerProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealerProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealerId = new SelectList(db.Dealers, "DealerId", "DealerName", dealerProducts.DealerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", dealerProducts.ProductId);
            return View(dealerProducts);
        }

        // GET: DealerProducts/Delete/5
        [Route("DealerProducts/Delete/id")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DealerProducts dealerProducts = db.DealerProducts.Find(id);
            if (dealerProducts == null)
            {
                return HttpNotFound();
            }
            return View(dealerProducts);
        }

        // POST: DealerProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DealerProducts dealerProducts = db.DealerProducts.Find(id);
            db.DealerProducts.Remove(dealerProducts);
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
