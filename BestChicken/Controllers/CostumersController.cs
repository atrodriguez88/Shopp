using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BestChicken.Models;

namespace BestChicken.Controllers
{
    public class CostumersController : Controller
    {
        private BestChickenContext db = new BestChickenContext();

        // GET: Costumers
        public ActionResult Index()
        {
            var costumers = db.Costumers.Include(c => c.PayForm);
            return View(costumers.ToList());
        }

        // GET: Costumers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumer costumer = db.Costumers.Find(id);
            if (costumer == null)
            {
                return HttpNotFound();
            }
            return View(costumer);
        }

        // GET: Costumers/Create
        public ActionResult Create()
        {
            ViewBag.PayFormId = new SelectList(db.PayForms, "PayFormId", "Type");
            return View();
        }

        // POST: Costumers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CostumerId,FirstName,LastName,BirthOfDate,Email,PayFormId")] Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                if (costumer.PayFormId != 0)
                {
                    db.Costumers.Add(costumer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PayFormId = new SelectList(db.PayForms, "PayFormId", "Type", costumer.PayFormId);
                    return View(costumer);
                }

                
            }

            ViewBag.PayFormId = new SelectList(db.PayForms, "PayFormId", "Type", costumer.PayFormId);
            return View(costumer);
        }

        // GET: Costumers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumer costumer = db.Costumers.Find(id);
            if (costumer == null)
            {
                return HttpNotFound();
            }
            ViewBag.PayFormId = new SelectList(db.PayForms, "PayFormId", "Type", costumer.PayFormId);
            return View(costumer);
        }

        // POST: Costumers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CostumerId,FirstName,LastName,BirthOfDate,Email,PayFormId")] Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costumer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PayFormId = new SelectList(db.PayForms, "PayFormId", "Type", costumer.PayFormId);
            return View(costumer);
        }

        // GET: Costumers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumer costumer = db.Costumers.Find(id);
            if (costumer == null)
            {
                return HttpNotFound();
            }
            return View(costumer);
        }

        // POST: Costumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Costumer costumer = db.Costumers.Find(id);
            db.Costumers.Remove(costumer);
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
