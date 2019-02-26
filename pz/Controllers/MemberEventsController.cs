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
    public class MemberEventsController : Controller
    {
        private PzContext db = new PzContext();

        // GET: MemberEvents
        public ActionResult Index()
        {
            var memberEvents = db.MemberEvents.Include(m => m.Event).Include(m => m.Profile);
            return View(memberEvents.ToList());
        }

        // GET: MemberEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberEvent memberEvent = db.MemberEvents.Find(id);
            if (memberEvent == null)
            {
                return HttpNotFound();
            }
            return View(memberEvent);
        }

        // GET: MemberEvents/Create/5
        public ActionResult Create(int eid)
        {
            MemberEvent memberEvent = new MemberEvent();
            Profile profile = db.Profiles.Single(p => p.Username == User.Identity.Name);
            memberEvent.Profile = profile;
            memberEvent.EventId = eid;

            if (ModelState.IsValid)
            {
                db.MemberEvents.Add(memberEvent);
                db.SaveChanges();
                return RedirectToAction("Details","EventModels", new {id = eid});
            }


           // ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName");
            return View();
        }

        // POST: MemberEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventId")] MemberEvent memberEvent)
            
        {
            
            if (ModelState.IsValid)
            {
                db.MemberEvents.Add(memberEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "ID", "Name", memberEvent.EventId);
            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName", memberEvent.ProfileId);
            return View(memberEvent);
        }

        // GET: MemberEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberEvent memberEvent = db.MemberEvents.Find(id);
            if (memberEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "ID", "Name", memberEvent.EventId);
            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName", memberEvent.ProfileId);
            return View(memberEvent);
        }

        // POST: MemberEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProfileId,EventId")] MemberEvent memberEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "ID", "Name", memberEvent.EventId);
            ViewBag.ProfileId = new SelectList(db.Profiles, "ID", "FirstName", memberEvent.ProfileId);
            return View(memberEvent);
        }

        // GET: MemberEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberEvent memberEvent = db.MemberEvents.Find(id);
            if (memberEvent == null)
            {
                return HttpNotFound();
            }
            return View(memberEvent);
        }

        // POST: MemberEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberEvent memberEvent = db.MemberEvents.Find(id);
            db.MemberEvents.Remove(memberEvent);
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
