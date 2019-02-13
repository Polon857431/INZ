using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pz.DAL;
using pz.Models;

namespace pz.Controllers
{
    public class SpotsController : Controller
    {
        private PzContext db = new PzContext();

        // GET: Spots
        public ActionResult Index()
        {
            var spots = db.Spots.Include(s => s.Event).Include(s => s.Point);
            return View(spots.ToList());
        }

        // GET: Spots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // GET: Spots/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "ID", "Name");
            ViewBag.PointId = new SelectList(db.Points, "ID", "Name");
            return View();
        }

        // POST: Spots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventId,PointId,Value")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Spots.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "ID", "Name", spot.EventId);
            ViewBag.PointId = new SelectList(db.Points, "ID", "Name", spot.PointId);
            return View(spot);
        }

        // GET: Spots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "ID", "Name", spot.EventId);
            ViewBag.PointId = new SelectList(db.Points, "ID", "Name", spot.PointId);
            return View(spot);
        }

        // POST: Spots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventId,PointId,Value")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "ID", "Name", spot.EventId);
            ViewBag.PointId = new SelectList(db.Points, "ID", "Name", spot.PointId);
            return View(spot);
        }

        // GET: Spots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: Spots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spot spot = db.Spots.Find(id);
            db.Spots.Remove(spot);
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
