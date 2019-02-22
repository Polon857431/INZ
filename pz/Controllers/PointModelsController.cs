using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using pz.DAL;
using pz.Models;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace pz.Controllers
{
    public class PointModelsController : Controller
    {
        private PzContext db = new PzContext();

        // GET: PointModels
        public ActionResult Index()
        {
            return View(db.Points.ToList());
        }

        // GET: PointModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointModel pointModel = db.Points.Find(id);
            if (pointModel == null)
            {
                return HttpNotFound();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCode qrCode = new QRCode(qrGenerator.CreateQrCode(pointModel.QR_code, QRCodeGenerator.ECCLevel.Q));
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View(pointModel);
        }

        // GET: PointModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PointModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,X,Y")] PointModel pointModel)
        {
            if (ModelState.IsValid)
            {
                pointModel.QR_code = pointModel.ID.ToString() + "_" + pointModel.Name;

                db.Points.Add(pointModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

       

            return View(pointModel);
        }

        // GET: PointModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointModel pointModel = db.Points.Find(id);
            if (pointModel == null)
            {
                return HttpNotFound();
            }
            return View(pointModel);
        }

        // POST: PointModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,X,Y")] PointModel pointModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pointModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pointModel);
        }

        // GET: PointModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointModel pointModel = db.Points.Find(id);
            if (pointModel == null)
            {
                return HttpNotFound();
            }
            return View(pointModel);
        }

        // POST: PointModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PointModel pointModel = db.Points.Find(id);
            db.Points.Remove(pointModel);
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
