using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MotorCycleWebsite.Models;

namespace MotorCycleWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private MotorCycleDBEntities db = new MotorCycleDBEntities();

        // GET: Products
        [Route("Products/Index")]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brands).Include(p => p.Categories);
            return View(products.ToList());
        }


        // GET: Products/Create
        [Route("Products/Create")]
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "CategorieName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Products/Create")]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,BrandId,CategorieId,Prize,Quantity,ImageUrl,Displacement")] Products products,HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    WebImage img3 = new WebImage(ImageUrl.InputStream);
                    FileInfo imginfo3 = new FileInfo(ImageUrl.FileName);

                    string productsImgName = Guid.NewGuid().ToString() + imginfo3.Extension;
                    img3.Resize(250, 250);
                    img3.Save("~/Uploads/Products/" + productsImgName);

                    products.ImageUrl = "/Uploads/Products/" + productsImgName;
                }
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", products.BrandId);
            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "CategorieName", products.CategorieId);
            return View(products);
        }

        // GET: Products/Edit/5
        [Route("Products/Edit/id")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", products.BrandId);
            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "CategorieName", products.CategorieId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,BrandId,CategorieId,Prize,Quantity,ImageUrl,Displacement")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", products.BrandId);
            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "CategorieName", products.CategorieId);
            return View(products);
        }

        // GET: Products/Delete/5
        [Route("Products/Delete/id")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
