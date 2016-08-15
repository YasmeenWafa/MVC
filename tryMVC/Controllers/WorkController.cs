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


    public class WorkLogic
    {
        public DBContext db = new DBContext();

        public WorkLogic()
        {

        }
        public List<WorkModel> ToList()
        {
            return db.Work.ToList();
        }

        public WorkModel find(int? id)
        {
            return db.Work.Find(id);
        }
        public void create(WorkModel workModel)
        {
            db.Work.Add(workModel);
            db.SaveChanges();
        }
        public void edit(WorkModel workModel)
        {
            db.Entry(workModel).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void remove(WorkModel workModel)
        {
            db.Work.Remove(workModel);
            db.SaveChanges();
        }
    }



    public class WorkController : Controller
    {
        //private DBContext db = new DBContext();
       public  WorkLogic wl = new WorkLogic();
        // GET: Work
        public ActionResult Index()
        {
            return View(wl.ToList());
        }

        // GET: Work/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkModel workModel = wl.find(id);
            if (workModel == null)
            {
                return HttpNotFound();
            }
            return View(workModel);
        }

        // GET: Work/Create
        public ActionResult Create()
        {
            ViewBag.customerList = new SelectList(wl.db.Customers, "customerName", "customerName");
            ViewBag.itemList = new SelectList(wl.db.ServiceItems, "serviceItemName", "serviceItemName");
            ViewBag.serviceList = new SelectList(wl.db.Services, "serviceName", "serviceName");
            
            return View();
        }

        // POST: Work/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "workID,price,serviceName,itemName,customerName")] WorkModel workModel)
        {
            if (ModelState.IsValid)
            {
                wl.create(workModel);
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
            WorkModel workModel = wl.find(id);
            if (workModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerList = new SelectList(wl.db.Customers, "customerName", "customerName");
            ViewBag.itemList = new SelectList(wl.db.ServiceItems, "serviceItemName", "serviceItemName");
            ViewBag.serviceList = new SelectList(wl.db.Services, "serviceName", "serviceName");
            return View(workModel);
        }

        // POST: Work/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "workID,price,serviceName,itemName,customerName")] WorkModel workModel)
        {
            if (ModelState.IsValid)
            {
                wl.edit(workModel);
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
            WorkModel workModel = wl.find(id);
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
            WorkModel workModel = wl.find(id);
            wl.remove(workModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                wl.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
