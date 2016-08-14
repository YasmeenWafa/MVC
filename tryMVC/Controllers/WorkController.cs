using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tryMVC.Models;

namespace tryMVC.Controllers
{
    public class WorkController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Work
        public ActionResult Index()
        {
            return View(db.Work.ToList());
        }

        // GET: Work/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkModel workModel = db.Work.Find(id);
            if (workModel == null)
            {
                return HttpNotFound();
            }
            return View(workModel);
        }

        // GET: Work/Create
        public ActionResult Create()
        {
            ViewBag.customerList = new SelectList(db.Customers, "customerName", "customerName");
            ViewBag.itemList = new SelectList(db.ServiceItems, "serviceItemName", "serviceItemName");
            ViewBag.serviceList = new SelectList(db.Services, "serviceName", "serviceName");
            
            return View();
        }

        // POST: Work/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "workID,serviceName,itemName,customerName,price")] WorkModel workModel)
        {
            if (ModelState.IsValid)
            {
                db.Work.Add(workModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workModel);
        }

        // GET: Work/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkModel workModel = db.Work.Find(id);
            if (workModel == null)
            {
                return HttpNotFound();
            }
            return View(workModel);
        }

        // POST: Work/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "workID,serviceName,itemName,customerName,price")] WorkModel workModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workModel);
        }

        // GET: Work/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkModel workModel = db.Work.Find(id);
            if (workModel == null)
            {
                return HttpNotFound();
            }
            return View(workModel);
        }

        // POST: Work/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkModel workModel = db.Work.Find(id);
            db.Work.Remove(workModel);
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
