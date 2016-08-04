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
    public class NationalitiesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Nationalities
        public ActionResult Index()
        {
            return View(db.Nationalities.ToList());
        }

        // GET: Nationalities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalitiesModel nationalitiesModel = db.Nationalities.Find(id);
            if (nationalitiesModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalitiesModel);
        }

        // GET: Nationalities/Create
        public ActionResult Create()
        {
              ViewBag.nationalityList = new SelectList(db.AllNationalities, "nationalityName", "nationalityName");

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
                if ((db.Nationalities.Any(ac => ac.nationalityName.Equals(nationalitiesModel.nationalityName))) )
   {
                    //TODO E.g. ModelState.AddModelError

                    return RedirectToAction("Create");
                }
                db.Nationalities.Add(nationalitiesModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nationalitiesModel);
        }

        // GET: Nationalities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalitiesModel nationalitiesModel = db.Nationalities.Find(id);
            if (nationalitiesModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalitiesModel);
        }

        // POST: Nationalities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nationalityID,nationalityName")] NationalitiesModel nationalitiesModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalitiesModel).State = EntityState.Modified;
                db.SaveChanges();
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
            NationalitiesModel nationalitiesModel = db.Nationalities.Find(id);
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
            NationalitiesModel nationalitiesModel = db.Nationalities.Find(id);
            db.Nationalities.Remove(nationalitiesModel);
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
