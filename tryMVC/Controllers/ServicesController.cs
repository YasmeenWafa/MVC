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


    public class ServicesLogic
    {
        public DBContext db = new DBContext();

        public ServicesLogic()
        {

        }
        public List<ServicesModel> ToList()
        {
            return db.Services.ToList();
        }

        public ServicesModel find(int? id)
        {
            return db.Services.Find(id);
        }
        public void create(ServicesModel servicesModel)
        {
            db.Services.Add(servicesModel);
            db.SaveChanges();
        }
        public void edit(ServicesModel servicesModel)
        {
            db.Entry(servicesModel).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void remove(ServicesModel servicesModel)
        {
            db.Services.Remove(servicesModel);
            db.SaveChanges();
        }
    }

    public class ServicesController : Controller
    {
        // private DBContext db = new DBContext();
        public ServicesLogic sl = new ServicesLogic();
        // GET: Services
        public ActionResult Index()
        {
            return View(sl.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesModel servicesModel = sl.find(id);
            if (servicesModel == null)
            {
                return HttpNotFound();
            }
            return View(servicesModel);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serviceID,serviceName")] ServicesModel servicesModel)
        {
            if (ModelState.IsValid)
            {
                sl.create(servicesModel);
                return RedirectToAction("Index");
            }

            return View(servicesModel);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesModel servicesModel = sl.find(id);
            if (servicesModel == null)
            {
                return HttpNotFound();
            }
            return View(servicesModel);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceID,serviceName")] ServicesModel servicesModel)
        {
            if (ModelState.IsValid)
            {
                sl.edit(servicesModel);
                return RedirectToAction("Index");
            }
            return View(servicesModel);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesModel servicesModel = sl.find(id);
            if (servicesModel == null)
            {
                return HttpNotFound();
            }
            return View(servicesModel);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicesModel servicesModel = sl.find(id);
            sl.remove(servicesModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sl.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
