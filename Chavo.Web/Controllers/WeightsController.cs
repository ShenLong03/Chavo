namespace Chavo.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Data;
    using Data.Entity;

    public class WeightsController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.Weights.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weight weight = await db.Weights.FindAsync(id);
            if (weight == null)
            {
                return HttpNotFound();
            }
            return View(weight);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Weight weight)
        {
            if (ModelState.IsValid)
            {
                db.Weights.Add(weight);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(weight);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weight weight = await db.Weights.FindAsync(id);
            if (weight == null)
            {
                return HttpNotFound();
            }
            return View(weight);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Weight weight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weight).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(weight);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weight weight = await db.Weights.FindAsync(id);
            if (weight == null)
            {
                return HttpNotFound();
            }

            db.Weights.Remove(weight);
            await db.SaveChangesAsync();
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
