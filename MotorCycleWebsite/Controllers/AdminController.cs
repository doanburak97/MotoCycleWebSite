using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorCycleWebsite.Models;

namespace MotorCycleWebsite.Controllers
{
    public class AdminController : Controller
    {

        MotorCycleDBEntities db = new MotorCycleDBEntities();

        // GET: Admin
        [Route("Admin/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Admin/Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            var login = db.Users.Where(x => x.Mail == user.Mail).SingleOrDefault();
            if (user.Mail == null && user.Password == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (login.Mail == user.Mail && login.Password == user.Password)
                {
                    Session["UserId"] = login.UserId;
                    Session["Name"] = login.Name;
                    Session["Surname"] = login.Surname;
                    Session["Mail"] = login.Mail;
                    Session["UserType"] = login.UserType;
                    return RedirectToAction("Index", "Admin");
                }
                ViewBag.Alert = "Email or Password isn't correct...";
                return View(user);
            }


        }

        [Route("Admin/Logout")]
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Mail"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
        
    }
}