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

    public class CustomersLogic
    {
        public DBContext db = new DBContext();

        public CustomersLogic()
        {

        }
        public List<CustomersModel> ToList()
        {
          return db.Customers.ToList();
        }

        public CustomersModel find(int? id)
        {
           return  db.Customers.Find(id);
        }
        public void create (CustomersModel customersModel)
        {
            db.Customers.Add(customersModel);
            db.SaveChanges();
        }
        public void edit(CustomersModel customersModel)
        {
            db.Entry(customersModel).State = EntityState.Modified;
            db.SaveChanges();
            
        }
        public void remove(CustomersModel customersModel)
        {
            db.Customers.Remove(customersModel);
            db.SaveChanges();
        }
    }
    public class CustomersController : Controller
    {
        //  private DBContext db = new DBContext();
        public CustomersLogic cl = new CustomersLogic();
        // GET: Customers
        public ActionResult Index()
        {
            return View(cl.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersModel customersModel = cl.find(id);
            if (customersModel == null)
            {
                return HttpNotFound();
            }
            return View(customersModel);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.nationalityList = new SelectList(cl.db.Nationalities, "nationalityName", "nationalityName");
            return View();

        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,customerName,customerAddress,customerAge,gender,customerEmail,nationalityName,phoneNumber")] CustomersModel customersModel)
        {
            if (ModelState.IsValid)
            {
                cl.create(customersModel);
              
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
            CustomersModel customersModel = cl.find(id);
            if (customersModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.nationalityList = new SelectList(cl.db.Nationalities, "nationalityName", "nationalityName");
            return View(customersModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID,customerName,customerAddress,customerAge,gender,customerEmail,phoneNumber,nationalityName")] CustomersModel customersModel)
        {
            if (ModelState.IsValid)
            {
                cl.edit(customersModel);
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
            CustomersModel customersModel = cl.find(id);
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
            CustomersModel customersModel =cl.find(id);
            cl.remove(customersModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cl.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}