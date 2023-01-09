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
    public class UserServiceesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserServicees
        public ActionResult Index(string searching)
        {
            return View(db.UserServicees.Where(x => x.Service.Contains(searching) || searching == null).ToList());
        }

        // GET: UserServicees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserServicee userServicee = db.UserServicees.Find(id);
            if (userServicee == null)
            {
                return HttpNotFound();
            }
            return View(userServicee);
        }

        // GET: UserServicees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserServicees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdvertiserName,Service,Email,Contacts,Location,image,File")] UserServicee userServicee)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(userServicee.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images2/"), _filename);
                userServicee.image = "~/Images2/" + _filename;
                db.UserServicees.Add(userServicee);

                if (userServicee.File.ContentLength < 1000000)
                {
                    if (db.SaveChanges() > 0)
                    {
                        userServicee.File.SaveAs(path);
                    }
                    return RedirectToAction("Index");

                }
                else { ViewBag.msg = "FILE TO LARGE"; }
            }

            return View(userServicee);
        }

        // GET: UserServicees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserServicee userServicee = db.UserServicees.Find(id);
            if (userServicee == null)
            {
                return HttpNotFound();
            }
            return View(userServicee);
        }

        // POST: UserServicees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdvertiserName,Service,Email,Contacts,Location,image")] UserServicee userServicee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userServicee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userServicee);
        }

        // GET: UserServicees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserServicee userServicee = db.UserServicees.Find(id);
            if (userServicee == null)
            {
                return HttpNotFound();
            }
            return View(userServicee);
        }

        // POST: UserServicees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserServicee userServicee = db.UserServicees.Find(id);
            db.UserServicees.Remove(userServicee);
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
