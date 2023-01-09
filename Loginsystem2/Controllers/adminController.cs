using Loginsystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loginsystem2.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
        [HttpGet]
        public ActionResult Admin()
        {
            return RedirectToAction("adminLogin");
        }
        Database2 db = new Database2();
        [HttpGet]
        public ActionResult adminRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminRegister(User1 us)
        {
            db.User1.Add(us);
            db.SaveChanges();
            return RedirectToAction("adminLogin");
        }
        [HttpGet]
        public ActionResult adminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminLogin(User1 us)
        {

            if (us.User_Name == "Admin" && us.Password == "#1234")
            {
                return RedirectToAction("Index", "UserServicees1");
            }

            return View();
        }
    }
}