using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chavo.Web.Data;
using Chavo.Web.Data.Entity;

namespace Chavo.Web.Controllers
{
    public class PicturesProductsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PicturesProducts
        public async Task<ActionResult> Index()
        {
            var picturesProducts = db.PicturesProducts.Include(p => p.Product);
            return View(await picturesProducts.ToListAsync());
        }

        // GET: PicturesProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PicturesProduct picturesProduct = await db.PicturesProducts.FindAsync(id);
            if (picturesProduct == null)
            {
                return HttpNotFound();
            }
            return View(picturesProduct);
        }

        // GET: PicturesProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: PicturesProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PicturesProductId,Name,Active,ProductId")] PicturesProduct picturesProduct)
        {
            if (ModelState.IsValid)
            {
                db.PicturesProducts.Add(picturesProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", picturesProduct.ProductId);
            return View(picturesProduct);
        }

        // GET: PicturesProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PicturesProduct picturesProduct = await db.PicturesProducts.FindAsync(id);
            if (picturesProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", picturesProduct.ProductId);
            return View(picturesProduct);
        }

        // POST: PicturesProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PicturesProductId,Name,Active,ProductId")] PicturesProduct picturesProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picturesProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", picturesProduct.ProductId);
            return View(picturesProduct);
        }

        // GET: PicturesProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PicturesProduct picturesProduct = await db.PicturesProducts.FindAsync(id);
            if (picturesProduct == null)
            {
                return HttpNotFound();
            }
            return View(picturesProduct);
        }

        // POST: PicturesProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PicturesProduct picturesProduct = await db.PicturesProducts.FindAsync(id);
            db.PicturesProducts.Remove(picturesProduct);
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
