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

    [Authorize]
    public class UnitLengthsController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.UnitLengths.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitLength unitLength = await db.UnitLengths.FindAsync(id);
            if (unitLength == null)
            {
                return HttpNotFound();
            }
            return View(unitLength);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UnitLength unitLength)
        {
            if (ModelState.IsValid)
            {
                db.UnitLengths.Add(unitLength);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unitLength);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitLength unitLength = await db.UnitLengths.FindAsync(id);
            if (unitLength == null)
            {
                return HttpNotFound();
            }
            return View(unitLength);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UnitLength unitLength)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitLength).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unitLength);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitLength unitLength = await db.UnitLengths.FindAsync(id);
            if (unitLength == null)
            {
                return HttpNotFound();
            }
   
            db.UnitLengths.Remove(unitLength);
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
