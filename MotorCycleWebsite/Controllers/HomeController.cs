using MotorCycleWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MotorCycleWebsite.Controllers
{
    public class HomeController : Controller
    {
        MotorCycleDBEntities db = new MotorCycleDBEntities();
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderId));
        }

        public ActionResult MCategories()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult MBrands()
        {
            return View(db.Brands.ToList());
        }

        public ActionResult MProducts()
        {
            return View(db.Products.ToList());
        }

        public ActionResult MContact()
        {
            return View(db.Users.ToList());
        }

        [HttpPost]
        public ActionResult MContact(string name=null,string email=null,string subject=null,string message=null )
        {
            if (name!=null && email!=null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "motorcyclewebinfo@gmail.com";
                WebMail.Password = "12341997";
                WebMail.SmtpPort = 587;
                WebMail.Send("motorcyclewebinfo@gmail.com", subject, email + "</br>" + message);
                ViewBag.Danger = "Message has been send.";
            }
            else
            {
                ViewBag.Danger = "Message didnt send.";
            }
            return View();
        }

        public ActionResult ProductDetail(int ? id)
        {
            return View(db.Products.Find(id));
        }
    }
}