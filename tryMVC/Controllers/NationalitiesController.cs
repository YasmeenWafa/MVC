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


    public class NationalitiesLogic
    {
        public DBContext db = new DBContext();

        public NationalitiesLogic()
        {

        }
        public List<NationalitiesModel> ToList()
        {
            return db.Nationalities.ToList();
        }

        public NationalitiesModel find(int? id)
        {
            return db.Nationalities.Find(id);
        }
        public void create(NationalitiesModel nationalitiesModel)
        {
            db.Nationalities.Add(nationalitiesModel);
            db.SaveChanges();
        }
        public void edit(NationalitiesModel nationalitiesModel)
        {
            db.Entry(nationalitiesModel).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void remove(NationalitiesModel nationalitiesModel)
        {
            db.Nationalities.Remove(nationalitiesModel);
            db.SaveChanges();
        }
    }


    public class NationalitiesController : Controller
    {
       // private DBContext db = new DBContext();
        public NationalitiesLogic nl = new NationalitiesLogic();
        // GET: Nationalities
        public ActionResult Index()
        {
            return View(nl.ToList());
        }

        // GET: Nationalities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalitiesModel nationalitiesModel = nl.find(id);
            if (nationalitiesModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalitiesModel);
        }

        // GET: Nationalities/Create
        public ActionResult Create()
        {
              ViewBag.nationalityList = new SelectList(nl.db.AllNationalities, "nationalityName", "nationalityName");

            return View();
        }

        // POST: Nationalities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nationalityID, nationalityName")] NationalitiesModel nationalitiesModel)
        {
            if (ModelState.IsValid)
            {
                if ((nl.db.Nationalities.Any(ac => ac.nationalityName.Equals(nationalitiesModel.nationalityName))) )
   {
                    //TODO E.g. ModelState.AddModelError

                    return RedirectToAction("Create");
                }
                nl.create(nationalitiesModel);
                return RedirectToAction("Index");
            }

            return View(nationalitiesModel);
        }

       

        // GET: Nationalities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalitiesModel nationalitiesModel = nl.find(id);
            if (nationalitiesModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalitiesModel);
        }

        // POST: Nationalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalitiesModel nationalitiesModel = nl.find(id);
            nl.remove(nationalitiesModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                nl.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
