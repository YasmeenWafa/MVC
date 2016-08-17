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


    public class ServiceItemsLogic
    {
        public DBContext db = new DBContext();

        public ServiceItemsLogic()
        {

        }
        public List<ServiceItemsModel> ToList()
        {
            return db.ServiceItems.ToList();
        }

        public ServiceItemsModel find(int? id)
        {
            return db.ServiceItems.Find(id);
        }
        public void create(ServiceItemsModel serviceItemsModel)
        {

            db.ServiceItems.Add(serviceItemsModel);
            db.SaveChanges();
        }
        public void edit(ServiceItemsModel serviceItemsModel)
        {
            db.Entry(serviceItemsModel).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void remove(ServiceItemsModel serviceItemsModel)
        {
            db.ServiceItems.Remove(serviceItemsModel);
            db.SaveChanges();
        }
    }


    public class ServiceItemsController : Controller
    {
        //private DBContext db = new DBContext();
        public ServiceItemsLogic sil = new ServiceItemsLogic();
        // GET: ServiceItems
        public ActionResult Index()
        {
            return View(sil.ToList());
        }

        // GET: ServiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceItemsModel serviceItemsModel = sil.find(id);
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
                List<ServiceItemsModel> allItems = sil.ToList();
                foreach(var item in allItems)
                {
                    if(item.serviceItemName.Equals(serviceItemsModel.serviceItemName))
                    {
                        
                        return View("Error",serviceItemsModel);
                    }
                }
                    
                
                sil.create(serviceItemsModel);
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
            ServiceItemsModel serviceItemsModel =sil.find(id);
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
                sil.edit(serviceItemsModel);
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
            ServiceItemsModel serviceItemsModel = sil.find(id);
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
            ServiceItemsModel serviceItemsModel = sil.find(id);
            sil.remove(serviceItemsModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sil.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
