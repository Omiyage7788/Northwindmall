using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWind.Models;

namespace NorthWind.Controllers
{

    
    public class ManagerController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account, string Password)
        {
            var user = db.Employees.Where(u => u.LastName == Account && u.FirstName == Password).FirstOrDefault();

            if (user == null) 
            {
                ViewBag.msg = "帳號或密碼有錯!";
                return View();
            }

            Session["user"] = user;
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
        }


    }
}