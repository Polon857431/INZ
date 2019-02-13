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
    public class MemberSpotsController : Controller
    {
        private PzContext db = new PzContext();

        // GET: MemberSpots
        public ActionResult Index()
        {
            var memberSpots = db.MemberSpots.Include(m => m.Profile).Include(m => m.VisitedPoint);
            return View(memberSpots.ToList());
        }

        // GET: MemberSpots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSpots memberSpots = db.MemberSpots.Find(id);
            if (memberSpots == null)
            {
                return HttpNotFound();
            }
            return View(memberSpots);
        }

        // GET: MemberSpots/Create
        public ActionResult Create(int sid) {
            MemberSpots memberSpots = new MemberSpots();
            Profile profile = db.Profiles.Single(p => p.Username == User.Identity.Name);
             
            
            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName");
            ViewBag.SpotId = new SelectList(db.Spots, "ID", "ID");
            return View();
        }

        // POST: MemberSpots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProfileId,SpotId")] MemberSpots memberSpots)
        {
            if (ModelState.IsValid)
            {
                db.MemberSpots.Add(memberSpots);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName", memberSpots.ProfileId);
            ViewBag.SpotId = new SelectList(db.Spots, "ID", "ID", memberSpots.SpotId);
            return View(memberSpots);
        }

        // GET: MemberSpots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSpots memberSpots = db.MemberSpots.Find(id);
            if (memberSpots == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName", memberSpots.ProfileId);
            ViewBag.SpotId = new SelectList(db.Spots, "ID", "ID", memberSpots.SpotId);
            return View(memberSpots);
        }

        // POST: MemberSpots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProfileId,SpotId")] MemberSpots memberSpots)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberSpots).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName", memberSpots.ProfileId);
            ViewBag.SpotId = new SelectList(db.Spots, "ID", "ID", memberSpots.SpotId);
            return View(memberSpots);
        }

        // GET: MemberSpots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSpots memberSpots = db.MemberSpots.Find(id);
            if (memberSpots == null)
            {
                return HttpNotFound();
            }
            return View(memberSpots);
        }

        // POST: MemberSpots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberSpots memberSpots = db.MemberSpots.Find(id);
            db.MemberSpots.Remove(memberSpots);
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
