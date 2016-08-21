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
            
            return db.Work.ToList<WorkModel>();
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

            ICollection<ServicesModel> temp = new List<ServicesModel>();
            foreach(var s in db.Work.Find(workModel.workID).service)
            {
                temp.Add(s);
            }
            foreach(var item in temp)
            {
                db.Work.Find(workModel.workID).service.Remove(item);
            }
            db.Work.Find(workModel.workID).service = workModel.service;
            db.Work.Find(workModel.workID).customer = workModel.customer;
            db.Work.Find(workModel.workID).item = workModel.item;
            db.Work.Find(workModel.workID).price = workModel.price;

            //db.Entry(workModel).State = EntityState.Modified;
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
            return View(wl.db.Work.ToList());
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
            ViewBag.customer = new SelectList(wl.db.Customers, "customerID", "customerName");
            ViewBag.item = new SelectList(wl.db.ServiceItems, "serviceItemID", "serviceItemName");
            ViewBag.service = new MultiSelectList(wl.db.Services, "serviceID", "serviceName");
            
            return View();
        }

        // POST: Work/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkModel workModel, int [] service,int customer,int item)
        {

            if(service!=null)
            {
                foreach(var id in service)
                {
                    ServicesModel ser = wl.db.Services.Find(id);
                    workModel.service.Add(ser);
                }
            }
      
            if(customer != 0)
            {
                CustomersModel cm = wl.db.Customers.Find(customer);
                workModel.customer = cm;
            }
            if (item != 0)
            {
                ServiceItemsModel sim = wl.db.ServiceItems.Find(item);
                workModel.item = sim;
            }


            wl.create(workModel);
               
                return RedirectToAction("Index",wl.db.Work);
           

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

            ViewBag.customer = new SelectList(wl.db.Customers, "customerID", "customerName");
            ViewBag.item = new SelectList(wl.db.ServiceItems, "serviceItemID", "serviceItemName");
            ViewBag.service = new MultiSelectList(wl.db.Services, "serviceID", "serviceName");


            return View(workModel);
        }

        // POST: Work/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkModel workModel, int[] service, int customer, int item)
        {
           
                if (service != null)
            {
                foreach (var id in service)
                {
                    ServicesModel ser = wl.db.Services.Find(id);
                    workModel.service.Add(ser);
                }
            }

                if (customer != 0)
                {
                    CustomersModel cm = wl.db.Customers.Find(customer);
                    workModel.customer = cm;
                }
                if (item != 0)
                {
                    ServiceItemsModel sim = wl.db.ServiceItems.Find(item);
                    workModel.item = sim;
                }


                wl.edit(workModel);
                return RedirectToAction("Index");
            



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
