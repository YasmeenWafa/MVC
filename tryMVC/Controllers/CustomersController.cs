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
    public class CustomersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersModel customersModel = db.Customers.Find(id);
            if (customersModel == null)
            {
                return HttpNotFound();
            }
            return View(customersModel);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.nationalityList = new SelectList(db.Nationalities, "nationalityName", "nationalityName");
            return View();
         
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,customerName,customerAddress,customerAge,gender,customerEmail,nationalityName")] CustomersModel customersModel)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customersModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customersModel);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersModel customersModel = db.Customers.Find(id);
            if (customersModel == null)
            {
                return HttpNotFound();
            }
            return View(customersModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID,customerName,customerAddress,customerAge,gender,customerEmail")] CustomersModel customersModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customersModel);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersModel customersModel = db.Customers.Find(id);
            if (customersModel == null)
            {
                return HttpNotFound();
            }
            return View(customersModel);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomersModel customersModel = db.Customers.Find(id);
            db.Customers.Remove(customersModel);
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
