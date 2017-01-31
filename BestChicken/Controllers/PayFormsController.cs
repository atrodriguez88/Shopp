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
    public class PayFormsController : Controller
    {
        private BestChickenContext db = new BestChickenContext();

        // GET: PayForms
        public ActionResult Index()
        {
            return View(db.PayForms.ToList());
        }

        // GET: PayForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayForm payForm = db.PayForms.Find(id);
            if (payForm == null)
            {
                return HttpNotFound();
            }
            return View(payForm);
        }

        // GET: PayForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayFormId,Type")] PayForm payForm)
        {
            if (ModelState.IsValid)
            {
                db.PayForms.Add(payForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payForm);
        }

        // GET: PayForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayForm payForm = db.PayForms.Find(id);
            if (payForm == null)
            {
                return HttpNotFound();
            }
            return View(payForm);
        }

        // POST: PayForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayFormId,Type")] PayForm payForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payForm);
        }

        // GET: PayForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayForm payForm = db.PayForms.Find(id);
            if (payForm == null)
            {
                return HttpNotFound();
            }
            return View(payForm);
        }

        // POST: PayForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayForm payForm = db.PayForms.Find(id);
            db.PayForms.Remove(payForm);
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
