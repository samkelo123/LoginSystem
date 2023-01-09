using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Loginsystem2.Models;

namespace Loginsystem2.Controllers
{
    public class UserServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserServices
        public ActionResult Index()
        {
            return View(db.UserServices.ToList());
        }

        // GET: UserServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserService userService = db.UserServices.Find(id);
            if (userService == null)
            {
                return HttpNotFound();
            }
            return View(userService);
        }

        // GET: UserServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdvertiserName,Service,Email,Contacts,Location,image,File")] UserService userService)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(userService.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                userService.image = "~/Images/" + _filename;
                db.UserServices.Add(userService);

                if (userService.File.ContentLength < 1000000)
                {
                    if (db.SaveChanges() > 0)
                    {
                        userService.File.SaveAs(path);
                    }
                    return RedirectToAction("Index");

                }
                else { ViewBag.msg = "FILE TO LARGE"; }

            }

            return View(userService);
        }

        // GET: UserServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserService userService = db.UserServices.Find(id);
            Session["imgPath"] = userService.image;
            if (userService == null)
            {
                return HttpNotFound();
            }
            return View(userService);
        }

        // POST: UserServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdvertiserName,Service,Email,Contacts,Location,image,File")] UserService userService)
        {
            if (ModelState.IsValid)
            {
                if (userService.File != null)
                {
                    string filename = Path.GetFileName(userService.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    userService.image = "~/Images/" + _filename;

                    if (userService.File.ContentLength < 1000000)
                    {
                        db.Entry(userService).State = EntityState.Modified;
                        string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                        if (db.SaveChanges() > 0)
                        {
                            userService.File.SaveAs(path);
                            if (System.IO.File.Exists(oldImgPath))
                            {
                                System.IO.File.Delete(oldImgPath);
                            }
                        }
                        return RedirectToAction("Index");

                    }
                    else { ViewBag.msg = "FILE TO LARGE"; }

                }
                else
                {
                    userService.image = Session["imgPath"].ToString();
                    db.Entry(userService).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(userService);
        }

        // GET: UserServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserService userService = db.UserServices.Find(id);
            if (userService == null)
            {
                return HttpNotFound();
            }
            return View(userService);
        }

        // POST: UserServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserService userService = db.UserServices.Find(id);
            db.UserServices.Remove(userService);
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
