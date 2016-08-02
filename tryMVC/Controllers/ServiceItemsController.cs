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
    public class ServiceItemsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ServiceItems
        public ActionResult Index()
        {
            return View(db.ServiceItems.ToList());
        }

        // GET: ServiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceItemsModel serviceItemsModel = db.ServiceItems.Find(id);
            if (serviceItemsModel == null)
            {
                return HttpNotFound();
            }
            return View(serviceItemsModel);
        }

        // GET: ServiceItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serviceItemID,serviceItemName")] ServiceItemsModel serviceItemsModel)
        {
            if (ModelState.IsValid)
            {
                db.ServiceItems.Add(serviceItemsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceItemsModel);
        }

        // GET: ServiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceItemsModel serviceItemsModel = db.ServiceItems.Find(id);
            if (serviceItemsModel == null)
            {
                return HttpNotFound();
            }
            return View(serviceItemsModel);
        }

        // POST: ServiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceItemID,serviceItemName")] ServiceItemsModel serviceItemsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceItemsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceItemsModel);
        }

        // GET: ServiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceItemsModel serviceItemsModel = db.ServiceItems.Find(id);
            if (serviceItemsModel == null)
            {
                return HttpNotFound();
            }
            return View(serviceItemsModel);
        }

        // POST: ServiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceItemsModel serviceItemsModel = db.ServiceItems.Find(id);
            db.ServiceItems.Remove(serviceItemsModel);
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
